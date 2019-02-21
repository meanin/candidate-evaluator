using System.Threading.Tasks;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Queries.InterviewResult;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Queries.InterviewResult
{
    public class GetInterviewResultHandler : IQueryHandler<GetInterviewResultQuery, Contract.Models.InterviewResult>
    {
        private readonly IInterviewResultRepository _interviewRepository;

        public GetInterviewResultHandler(IInterviewResultRepository interviewRepository)
        {
            _interviewRepository = interviewRepository;
        }

        public Task<Contract.Models.InterviewResult> Handle(GetInterviewResultQuery query)
        {
            return _interviewRepository.Get(query.OwnerId, query.Id);
        }
    }
}
