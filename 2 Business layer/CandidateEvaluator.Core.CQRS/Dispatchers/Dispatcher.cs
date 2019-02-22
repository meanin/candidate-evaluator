using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.CQRS.Commands;
using CandidateEvaluator.Contract.CQRS.Dispatchers;
using CandidateEvaluator.Contract.CQRS.Queries;

namespace CandidateEvaluator.Core.CQRS.Dispatchers
{
    public class Dispatcher : IDispatcher
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public Dispatcher(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        public Task<TResult> Query<TResult>(IQuery<TResult> query)
        {
            return _queryDispatcher.Query(query);
        }

        public Task<Guid> Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            return _commandDispatcher.Send(command);
        }
    }
}
