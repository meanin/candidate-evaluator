using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Contract.Queries.InterviewResult;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Queries.InterviewResult
{
    public class GetAllInterviewResultsHandler : IQueryHandler<GetAllInterviewResultsQuery, IEnumerable<Contract.Models.InterviewResult>>
    {
        private readonly IInterviewResultRepository _modelRepository;

        public GetAllInterviewResultsHandler(IInterviewResultRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public Task<IEnumerable<Contract.Models.InterviewResult>> Handle(GetAllInterviewResultsQuery query)
        {
            return _modelRepository.GetAll(query.OwnerId);
        }
    }
}
