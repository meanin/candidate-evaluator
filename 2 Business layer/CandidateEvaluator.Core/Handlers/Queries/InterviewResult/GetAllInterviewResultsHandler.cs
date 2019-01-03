using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Dtos;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Queries.Interview;
using CandidateEvaluator.Contract.Queries.InterviewResult;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Queries.InterviewResult
{
    public class GetAllInterviewResultsHandler : IQueryHandler<GetAllInterviewResults, List<Contract.Models.InterviewResult>>
    {
        private readonly IInterviewResultRepository _modelRepository;

        public GetAllInterviewResultsHandler(IInterviewResultRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public Task<List<Contract.Models.InterviewResult>> Handle(GetAllInterviewResults query)
        {
            return _modelRepository.GetAll(query.OwnerId);
        }
    }
}
