using System.Threading.Tasks;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Contract.Interview.Queries.InterviewResult;
using CandidateEvaluator.Contract.Interview.Repositories;

namespace CandidateEvaluator.Core.Interview.QueryHandlers.InterviewResult
{
    public class GetInterviewResultHandler : IQueryHandler<GetInterviewResultQuery, Contract.Interview.Models.InterviewResult>
    {
        private readonly IInterviewResultRepository _interviewRepository;

        public GetInterviewResultHandler(IInterviewResultRepository interviewRepository)
        {
            _interviewRepository = interviewRepository;
        }

        public Task<Contract.Interview.Models.InterviewResult> Handle(GetInterviewResultQuery query)
        {
            return _interviewRepository.Get(query.OwnerId, query.Id);
        }
    }
}
