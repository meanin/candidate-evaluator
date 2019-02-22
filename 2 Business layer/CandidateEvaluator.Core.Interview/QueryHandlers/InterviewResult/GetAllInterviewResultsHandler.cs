using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Contract.Interview.Queries.InterviewResult;
using CandidateEvaluator.Contract.Interview.Repositories;

namespace CandidateEvaluator.Core.Interview.QueryHandlers.InterviewResult
{
    public class GetAllInterviewResultsHandler : IQueryHandler<GetAllInterviewResultsQuery, IEnumerable<Contract.Interview.Models.InterviewResult>>
    {
        private readonly IInterviewResultRepository _modelRepository;

        public GetAllInterviewResultsHandler(IInterviewResultRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public Task<IEnumerable<Contract.Interview.Models.InterviewResult>> Handle(GetAllInterviewResultsQuery query)
        {
            return _modelRepository.GetAll(query.OwnerId);
        }
    }
}
