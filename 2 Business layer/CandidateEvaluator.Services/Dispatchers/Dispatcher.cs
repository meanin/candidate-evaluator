using CandidateEvaluator.Contract.Commands;
using CandidateEvaluator.Contract.Dispatchers;
using CandidateEvaluator.Contract.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateEvaluator.Services.Dispatchers
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

        public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            return _queryDispatcher.QueryAsync(query);
        }

        public Task<Guid> SendAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            return _commandDispatcher.SendAsync(command);
        }
    }
}
