using CandidateEvaluator.Contract.Commands;
using CandidateEvaluator.Contract.Dispatchers;
using CandidateEvaluator.Contract.Handlers;
using System;
using System.Threading.Tasks;

namespace CandidateEvaluator.Core.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Guid> SendAsync<T>(T command) where T : ICommand
        {
            var handler = (ICommandHandler<T>)_serviceProvider.GetService(typeof(ICommandHandler<T>));
            return await handler.HandleAsync(command);
        }
    }
}
