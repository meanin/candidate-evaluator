using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.CQRS.Commands;

namespace CandidateEvaluator.Contract.CQRS.Handlers
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task<Guid> Handle(T command);
    }
}
