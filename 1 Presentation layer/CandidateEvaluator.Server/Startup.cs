using CandidateEvaluator.Contract.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Blazor.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using CandidateEvaluator.Server.Extensions;

namespace CandidateEvaluator.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var atsConfig = new AzureTableStorageOptions();
            Configuration.Bind("AzureTableStorageOptions", atsConfig);
            services.AddSingleton(atsConfig);
            var aadOptions = new AadOptions();
            Configuration.Bind("AadOptions", aadOptions);
            services.AddSingleton(aadOptions);
            var mailOptions = new MailOptions();
            Configuration.Bind("MailOptions", mailOptions);
            services.AddSingleton(mailOptions);
            services
                .AddAuthentication(sharedOptions =>
                {
                    sharedOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Audience = aadOptions.ClientId;
                    options.Authority = $"https://login.microsoftonline.com/{aadOptions.TenantId}/";
                });

            services.AddSingleton<HttpClient>();

            services.AddCqrs();
            services.AddCoreObjects();
            services.AddInterview();
            services.AddAccount();

            services.AddMvc();

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet,
                    WasmMediaTypeNames.Application.Wasm,
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
            });

            app.UseBlazor<Client.Startup>();
        }
    }
}
