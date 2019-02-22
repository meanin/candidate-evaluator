using CandidateEvaluator.Contract.CQRS.Dispatchers;
using CandidateEvaluator.Core.CQRS.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace CandidateEvaluator.Server.Extensions
{
    public static class RegisterCqrsExtensions
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services)
        {
            services.AddTransient<IDispatcher, Dispatcher>();
            services.AddTransient<ICommandDispatcher, CommandDispatcher>();
            services.AddTransient<IQueryDispatcher, QueryDispatcher>();

            return services;
        }
    }
}
