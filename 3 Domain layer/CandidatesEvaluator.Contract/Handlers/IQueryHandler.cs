using CandidateEvaluator.Contract.Queries;
using System.Threading.Tasks;

namespace CandidateEvaluator.Contract.Handlers
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(TQuery query);
    }
}
