using System.Collections.Generic;
using CandidateEvaluator.Contract.Account.Models;
using CandidateEvaluator.Contract.Account.Queries;
using CandidateEvaluator.Contract.Account.Repositories;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Core.Account.QueryHandlers;
using CandidateEvaluator.Data.Account.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CandidateEvaluator.Server.Extensions
{
    public static class RegisterAccountExtensions
    {
        public static IServiceCollection AddAccount(this IServiceCollection services)
        {
            services.AddTransient<IUserRecentActivityRepository, UserRecentActivityRepository>();
            services.AddTransient<IQueryHandler<GetAllUserActivitiesQuery, IEnumerable<RecentActivity>>, GetAllUserActivitiesHandler>();

            return services;
        }
    }
}
