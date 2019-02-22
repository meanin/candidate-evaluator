using System.Threading.Tasks;
using CandidateEvaluator.Contract.CQRS.Queries;

namespace CandidateEvaluator.Contract.CQRS.Handlers
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(TQuery query);
    }
}
