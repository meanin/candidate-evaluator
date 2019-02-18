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
using CandidateEvaluator.Contract.Commands.Interview;
using CandidateEvaluator.Contract.Commands.InterviewResult;
using CandidateEvaluator.Contract.Dtos;
using CandidateEvaluator.Contract.Queries.Category;
using CandidateEvaluator.Contract.Queries.Interview;
using CandidateEvaluator.Contract.Queries.InterviewResult;
using CandidateEvaluator.Contract.Services;
using CandidateEvaluator.Core.Handlers.Commands.Category;
using CandidateEvaluator.Core.Handlers.Commands.Interview;
using CandidateEvaluator.Core.Handlers.Commands.InterviewResult;
using CandidateEvaluator.Core.Handlers.Commands.Question;
using CandidateEvaluator.Core.Handlers.Queries.Category;
using CandidateEvaluator.Core.Handlers.Queries.Interview;
using CandidateEvaluator.Core.Handlers.Queries.InterviewResult;
using CandidateEvaluator.Core.Handlers.Queries.Question;
using CandidateEvaluator.Core.Services;

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
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IUserRecentActivityRepository, UserRecentActivityRepository>();
            services.AddTransient<IInterviewRepository, InterviewRepository>();
            services.AddTransient<IInterviewResultRepository, InterviewResultRepository>();

            services.AddTransient<IMailClient, SendGridMailClient>();

            services.AddTransient<IDispatcher, Dispatcher>();
            services.AddTransient<ICommandDispatcher, CommandDispatcher>();
            services.AddTransient<IQueryDispatcher, QueryDispatcher>();

            services.AddTransient<ICommandHandler<CreateCategory>, CreateCategoryHandler>();
            services.AddTransient<ICommandHandler<DeleteCategory>, DeleteCategoryHandler>();
            services.AddTransient<ICommandHandler<UpdateCategory>, UpdateCategoryHandler>();

            services.AddTransient<ICommandHandler<CreateQuestion>, CreateQuestionHandler>();
            services.AddTransient<ICommandHandler<DeleteQuestion>, DeleteQuestionHandler>();
            services.AddTransient<ICommandHandler<UpdateQuestion>, UpdateQuestionHandler>();

            services.AddTransient<ICommandHandler<CreateInterview>, CreateInterviewHandler>();
            services.AddTransient<ICommandHandler<DeleteInterview>, DeleteInterviewHandler>();
            services.AddTransient<ICommandHandler<UpdateInterview>, UpdateInterviewHandler>();

            services.AddTransient<ICommandHandler<CreateInterviewResult>, CreateInterviewResultHandler>();
            services.AddTransient<ICommandHandler<DeleteInterviewResult>, DeleteInterviewResultHandler>();

            services.AddTransient<IQueryHandler<GetAllCategories, IEnumerable<Category>>, GetAllCategoriesHandler>();
            services.AddTransient<IQueryHandler<GetCategory, Category>, GetCategoryHandler>();
            services.AddTransient<IQueryHandler<GetQuestions, IEnumerable<Question>>, GetQuestionsHandler>();
            services.AddTransient<IQueryHandler<GetQuestion, Question>, GetQuestionHandler>();
            services.AddTransient<IQueryHandler<GetInterview, InterviewDto>, GetInterviewHandler>();
            services.AddTransient<IQueryHandler<GetAllInterviews, InterviewListDto>, GetAllInterviewsHandler>();
            services.AddTransient<IQueryHandler<GetAllUserActivities, IEnumerable<RecentActivity>>, GetAllUserActivitiesHandler>();
            services.AddTransient<IQueryHandler<GetAllInterviewResults, IEnumerable<InterviewResult>>, GetAllInterviewResultsHandler>();
            services.AddTransient<IQueryHandler<GetInterviewResult, InterviewResult>, GetInterviewResultHandler>();

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
