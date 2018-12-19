using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Dtos;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Queries.Interview;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Queries.Interview
{
    public class GetInterviewHandler : IQueryHandler<GetInterview, InterviewDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IInterviewRepository _interviewRepository;
        private readonly IQuestionRepository _questionRepository;

        public GetInterviewHandler(
            IInterviewRepository interviewRepository,
            ICategoryRepository categoryRepository,
            IQuestionRepository questionRepository)
        {
            _categoryRepository = categoryRepository;
            _interviewRepository = interviewRepository;
            _questionRepository = questionRepository;
        }

        public async Task<InterviewDto> HandleAsync(GetInterview query)
        {
            var model = await _interviewRepository.Get(query.OwnerId, query.Id);
            var dto = new InterviewDto
            {
                Name = model.Name,
                Id = model.Id,
                OwnerId = model.OwnerId,
                Content = new Dictionary<Contract.Models.Category, List<Contract.Models.Question>>()
            };

            foreach (var categoryId in model.Content.Keys)
            {
                var category = await _categoryRepository.Get(query.OwnerId, categoryId);
                dto.Content[category] = new List<Contract.Models.Question>();
                var questionIds = model.Content[categoryId];
                foreach (var questionId in questionIds)
                {
                    dto.Content[category].Add(await _questionRepository.Get(query.OwnerId, categoryId, questionId));
                }
            }

            return dto;
        }
    }
}
