using System;
using System.Collections.Generic;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Contract.Interview.Commands.Interview;
using CandidateEvaluator.Contract.Interview.Commands.InterviewResult;
using CandidateEvaluator.Contract.Interview.Factories;
using CandidateEvaluator.Contract.Interview.Models;
using CandidateEvaluator.Contract.Interview.Queries.Interview;
using CandidateEvaluator.Contract.Interview.Queries.InterviewResult;
using CandidateEvaluator.Contract.Interview.Repositories;
using CandidateEvaluator.Contract.Interview.Services;
using CandidateEvaluator.Core.Interview.CommandHandlers.Interview;
using CandidateEvaluator.Core.Interview.CommandHandlers.InterviewResult;
using CandidateEvaluator.Core.Interview.Factories;
using CandidateEvaluator.Core.Interview.QueryHandlers.Interview;
using CandidateEvaluator.Core.Interview.QueryHandlers.InterviewResult;
using CandidateEvaluator.Core.Interview.Services;
using CandidateEvaluator.Data.Interview.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CandidateEvaluator.Server.Extensions
{
    public static class RegisterInterviewExtensions
    {
        public static IServiceCollection AddInterview(this IServiceCollection services)
        {
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

            services.AddTransient<IQueryHandler<GetInterviewQuery, Interview>, GetInterviewHandler>();
            services.AddTransient<IQueryHandler<StartInterviewQuery, StartInterview>, StartInterviewHandler>();
            services.AddTransient<IQueryHandler<GetAllInterviewsQuery, List<(Guid Id, string Name)>>, GetAllInterviewsHandler>();

            services.AddTransient<IQueryHandler<GetAllInterviewResultsQuery, IEnumerable<InterviewResult>>, GetAllInterviewResultsHandler>();
            services.AddTransient<IQueryHandler<GetInterviewResultQuery, InterviewResult>, GetInterviewResultHandler>();

            return services;
        }
    }
}
