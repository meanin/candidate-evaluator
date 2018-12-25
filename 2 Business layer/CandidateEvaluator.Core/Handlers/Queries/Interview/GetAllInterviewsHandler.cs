using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Dtos;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Queries.Interview;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Queries.Interview
{
    public class GetAllInterviewsHandler : IQueryHandler<GetAllInterviews, InterviewListDto>
    {
        private readonly IInterviewRepository _modelRepository;

        public GetAllInterviewsHandler(IInterviewRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task<InterviewListDto> HandleAsync(GetAllInterviews query)
        {
            var interviews = await _modelRepository.GetAll(query.OwnerId);
            return new InterviewListDto
            {
                List = interviews.Select(i => new InterviewListDto.InterviewListElement
                {
                    Name = i.Name,
                    Id = i.Id
                }).ToList()
            };
        }
    }
}
