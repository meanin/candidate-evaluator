using CandidateEvaluator.Contract.Commands;
using System;
using System.Threading.Tasks;

namespace CandidateEvaluator.Contract.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task<Guid> Send<T>(T command) where T : ICommand;
    }
}
