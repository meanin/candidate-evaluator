using CandidateEvaluator.Contract.Commands.Category;
using CandidateEvaluator.Contract.Commands.Question;
using CandidateEvaluator.Contract.Configuration;
using CandidateEvaluator.Contract.Dispatchers;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Queries.Question;
using CandidateEvaluator.Contract.Queries.UserActivity;
using CandidateEvaluator.Contract.Repositories;
using CandidateEvaluator.Core.Dispatchers;
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
using CandidateEvaluator.Contract.Queries.Category;
using CandidateEvaluator.Core.Handlers.Commands.Category;
using CandidateEvaluator.Core.Handlers.Commands.Question;
using CandidateEvaluator.Core.Handlers.Queries.Category;
using CandidateEvaluator.Core.Handlers.Queries.Question;

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
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IUserRecentActivityRepository, UserRecentActivityRepository>();

            services.AddTransient<IDispatcher, Dispatcher>();
            services.AddTransient<ICommandDispatcher, CommandDispatcher>();
            services.AddTransient<IQueryDispatcher, QueryDispatcher>();

            services.AddTransient<ICommandHandler<CreateCategory>, CreateCategoryHandler>();
            services.AddTransient<ICommandHandler<DeleteCategory>, DeleteCategoryHandler>();
            services.AddTransient<ICommandHandler<UpdateCategory>, UpdateCategoryHandler>();

            services.AddTransient<ICommandHandler<CreateQuestion>, CreateQuestionHandler>();
            services.AddTransient<ICommandHandler<DeleteQuestion>, DeleteQuestionHandler>();
            services.AddTransient<ICommandHandler<UpdateQuestion>, UpdateQuestionHandler>();

            services.AddTransient<IQueryHandler<GetAllCategories, List<Category>>, GetAllCategoriesHandler>();
            services.AddTransient<IQueryHandler<GetCategory, Category>, GetCategoryHandler>();
            services.AddTransient<IQueryHandler<GetQuestionsFromCategory, List<Question>>, GetQuestionsFromCategoryHandler>();
            services.AddTransient<IQueryHandler<GetQuestion, Question>, GetQuestionHandler>();
            services.AddTransient<IQueryHandler<GetAllUserActivities, List<RecentActivity>>, GetAllUserActivitiesHandler>();

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
