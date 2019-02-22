using System.Threading.Tasks;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Contract.Interview.Queries.Interview;
using CandidateEvaluator.Contract.Interview.Repositories;

namespace CandidateEvaluator.Core.Interview.QueryHandlers.Interview
{
    public class GetInterviewHandler : IQueryHandler<GetInterviewQuery, Contract.Interview.Models.Interview>
    {
        private readonly IInterviewRepository _interviewRepository;

        public GetInterviewHandler(
            IInterviewRepository interviewRepository)
        {
            _interviewRepository = interviewRepository;
        }

        public Task<Contract.Interview.Models.Interview> Handle(GetInterviewQuery query)
        {
            return _interviewRepository.Get(query.OwnerId, query.Id);
        }
    }
}
