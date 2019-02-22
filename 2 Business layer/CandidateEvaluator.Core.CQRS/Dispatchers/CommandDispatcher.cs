using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.CQRS.Commands;
using CandidateEvaluator.Contract.CQRS.Dispatchers;
using CandidateEvaluator.Contract.CQRS.Handlers;

namespace CandidateEvaluator.Core.CQRS.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Guid> Send<T>(T command) where T : ICommand
        {
            var handler = (ICommandHandler<T>)_serviceProvider.GetService(typeof(ICommandHandler<T>));
            return await handler.Handle(command);
        }
    }
}
