using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Queries.InterviewResult;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Queries.InterviewResult
{
    public class GetAllInterviewResultsHandler : IQueryHandler<GetAllInterviewResults, IEnumerable<Contract.Models.InterviewResult>>
    {
        private readonly IInterviewResultRepository _modelRepository;

        public GetAllInterviewResultsHandler(IInterviewResultRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public Task<IEnumerable<Contract.Models.InterviewResult>> Handle(GetAllInterviewResults query)
        {
            return _modelRepository.GetAll(query.OwnerId);
        }
    }
}
