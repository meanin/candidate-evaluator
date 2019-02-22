using System;
using CandidateEvaluator.Contract.Configuration;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Queries.UserActivity;
using CandidateEvaluator.Contract.Repositories;
using CandidateEvaluator.Core.Handlers.Queries;
using CandidateEvaluator.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Blazor.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using CandidateEvaluator.Contract.Commands.Interview;
using CandidateEvaluator.Contract.Commands.InterviewResult;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Contract.Dtos;
using CandidateEvaluator.Contract.Factories;
using CandidateEvaluator.Contract.Queries.Interview;
using CandidateEvaluator.Contract.Queries.InterviewResult;
using CandidateEvaluator.Contract.Services;
using CandidateEvaluator.Core.Factories;
using CandidateEvaluator.Core.Handlers.Commands.Interview;
using CandidateEvaluator.Core.Handlers.Commands.InterviewResult;
using CandidateEvaluator.Core.Handlers.Queries.Interview;
using CandidateEvaluator.Core.Handlers.Queries.InterviewResult;
using CandidateEvaluator.Core.Services;
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

            services.AddTransient<IUserRecentActivityRepository, UserRecentActivityRepository>();
            services.AddTransient<IInterviewRepository, InterviewRepository>();
            services.AddTransient<IInterviewResultRepository, InterviewResultRepository>();

            services.AddTransient<IMailContentServiceFactory, UserPreferencesMailContentServiceFactory>();
            services.AddTransient<IMailService, SendGridMailService>();

            services.AddTransient<ICommandHandler<CreateInterviewCommand>, CreateInterviewHandler>();
            services.AddTransient<ICommandHandler<DeleteInterviewCommand>, DeleteInterviewHandler>();
            services.AddTransient<ICommandHandler<UpdateInterviewCommand>, UpdateInterviewHandler>();

            services.AddTransient<ICommandHandler<CreateInterviewResultCommand>, CreateInterviewResultHandler>();
            services.AddTransient<ICommandHandler<DeleteInterviewResultCommand>, DeleteInterviewResultHandler>();
            services.AddTransient<ICommandHandler<SendInterviewReportViaMailCommand>, SendInterviewReportViaMailHandler>();

            services.AddTransient<IQueryHandler<GetInterviewQuery, InterviewDto>, GetInterviewHandler>();
            services.AddTransient<IQueryHandler<GetAllInterviewsQuery, List<(Guid Id, string Name)>>, GetAllInterviewsHandler>();
            services.AddTransient<IQueryHandler<GetAllUserActivitiesQuery, IEnumerable<RecentActivity>>, GetAllUserActivitiesHandler>();
            services.AddTransient<IQueryHandler<GetAllInterviewResultsQuery, IEnumerable<InterviewResult>>, GetAllInterviewResultsHandler>();
            services.AddTransient<IQueryHandler<GetInterviewResultQuery, InterviewResult>, GetInterviewResultHandler>();

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
