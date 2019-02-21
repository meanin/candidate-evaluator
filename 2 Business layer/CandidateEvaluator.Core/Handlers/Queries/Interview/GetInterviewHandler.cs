using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Dtos;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Queries.Interview;
using CandidateEvaluator.Contract.Repositories;
using CandidateEvaluator.Core.Extensions;

namespace CandidateEvaluator.Core.Handlers.Queries.Interview
{
    public class GetInterviewHandler : IQueryHandler<GetInterviewQuery, InterviewDto>
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

        public async Task<InterviewDto> Handle(GetInterviewQuery query)
        {
            var model = await _interviewRepository.Get(query.OwnerId, query.Id);
            var dto = new InterviewDto
            {
                Name = model.Name,
                Id = model.Id,
                OwnerId = model.OwnerId,
                Content = new List<InterviewContentDto>()
            };

            foreach (var categoryId in model.Content.Keys)
            {
                var category = await _categoryRepository.Get(query.OwnerId, categoryId);
                var categoryQuestions = await _questionRepository.GetAllFromCategory(query.OwnerId, categoryId);
                dto.Content.Add(new InterviewContentDto
                {
                    Category = category,
                    Questions = categoryQuestions.Shuffle().Take(model.Content[categoryId]).ToList()
                });
            }
            return dto;
        }
    }
}
