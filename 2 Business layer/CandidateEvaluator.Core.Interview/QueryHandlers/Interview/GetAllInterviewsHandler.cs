using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Contract.Interview.Queries.Interview;
using CandidateEvaluator.Contract.Interview.Repositories;

namespace CandidateEvaluator.Core.Interview.QueryHandlers.Interview
{
    public class GetAllInterviewsHandler : IQueryHandler<GetAllInterviewsQuery, List<(Guid Id, string Name)>>
    {
        private readonly IInterviewRepository _modelRepository;

        public GetAllInterviewsHandler(IInterviewRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task<List<(Guid Id, string Name)>> Handle(GetAllInterviewsQuery query)
        {
            var interviews = await _modelRepository.GetAll(query.OwnerId);
            return interviews.Select(i => (i.Id, i.Name)).ToList();
        }
    }
}
