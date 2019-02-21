using CandidateEvaluator.Contract.Queries;
using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.CQRS.Dispatchers;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Contract.CQRS.Queries;

namespace CandidateEvaluator.Core.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> Query<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = _serviceProvider.GetService(handlerType);
            return await handler.Handle((dynamic)query);
        }
    }
}
