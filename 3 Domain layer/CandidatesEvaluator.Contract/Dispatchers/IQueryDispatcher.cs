using CandidateEvaluator.Contract.Queries;
using System.Threading.Tasks;

namespace CandidateEvaluator.Contract.Dispatchers
{
    public interface IQueryDispatcher
    {
        Task<TResult> Query<TResult>(IQuery<TResult> query);
    }
}
