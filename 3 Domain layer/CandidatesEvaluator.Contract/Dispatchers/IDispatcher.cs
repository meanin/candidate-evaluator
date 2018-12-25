using CandidateEvaluator.Contract.Commands;
using CandidateEvaluator.Contract.Queries;
using System;
using System.Threading.Tasks;

namespace CandidateEvaluator.Contract.Dispatchers
{
    public interface IDispatcher
    {
        Task<Guid> SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}
