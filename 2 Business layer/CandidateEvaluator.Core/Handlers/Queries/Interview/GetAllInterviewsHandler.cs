using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Queries.Interview;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Queries.Interview
{
    public class GetAllInterviewsHandler : IQueryHandler<GetAllInterviews, List<Contract.Models.Interview>>
    {
        private readonly IInterviewRepository _modelRepository;

        public GetAllInterviewsHandler(IInterviewRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public Task<List<Contract.Models.Interview>> HandleAsync(GetAllInterviews query)
        {
            return _modelRepository.GetAll(query.OwnerId);
        }
    }
}
