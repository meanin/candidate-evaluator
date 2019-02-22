using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.CoreObjects.Repositories;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Contract.Interview.Models;
using CandidateEvaluator.Contract.Interview.Queries.Interview;
using CandidateEvaluator.Contract.Interview.Repositories;
using CandidateEvaluator.Core.Utils.Extensions;

namespace CandidateEvaluator.Core.Interview.QueryHandlers.Interview
{
    public class StartInterviewHandler : IQueryHandler<StartInterviewQuery, StartInterview>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IInterviewRepository _interviewRepository;
        private readonly IQuestionRepository _questionRepository;

        public StartInterviewHandler(
            IInterviewRepository interviewRepository,
            ICategoryRepository categoryRepository,
            IQuestionRepository questionRepository)
        {
            _categoryRepository = categoryRepository;
            _interviewRepository = interviewRepository;
            _questionRepository = questionRepository;
        }

        public async Task<StartInterview> Handle(StartInterviewQuery query)
        {
            var model = await _interviewRepository.Get(query.OwnerId, query.Id);
            var dto = new StartInterview
            {
                Name = model.Name,
                Id = model.Id,
                OwnerId = model.OwnerId,
                Content = new List<StartInterviewContent>()
            };

            foreach (var categoryId in model.Content.Keys)
            {
                var category = await _categoryRepository.Get(query.OwnerId, categoryId);
                var categoryQuestions = await _questionRepository.GetAllFromCategory(query.OwnerId, categoryId);
                dto.Content.Add(new StartInterviewContent
                {
                    Category = category,
                    Questions = categoryQuestions.Shuffle().Take(model.Content[categoryId]).ToList()
                });
            }
            return dto;
        }
    }
}
