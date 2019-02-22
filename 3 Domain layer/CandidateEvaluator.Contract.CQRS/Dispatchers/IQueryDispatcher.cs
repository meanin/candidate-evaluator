using System.Threading.Tasks;
using CandidateEvaluator.Contract.CQRS.Queries;

namespace CandidateEvaluator.Contract.CQRS.Dispatchers
{
    public interface IQueryDispatcher
    {
        Task<TResult> Query<TResult>(IQuery<TResult> query);
    }
}
