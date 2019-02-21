using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.CQRS.Commands;

namespace CandidateEvaluator.Contract.CQRS.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task<Guid> Send<T>(T command) where T : ICommand;
    }
}
