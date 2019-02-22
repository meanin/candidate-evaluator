using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.CQRS.Commands;
using CandidateEvaluator.Contract.CQRS.Queries;

namespace CandidateEvaluator.Contract.CQRS.Dispatchers
{
    public interface IDispatcher
    {
        Task<Guid> Send<TCommand>(TCommand command) where TCommand : ICommand;
        Task<TResult> Query<TResult>(IQuery<TResult> query);
    }
}
