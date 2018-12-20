using System;
using System.Collections.Generic;
using System.Linq;
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
        private static Random _random = new Random();

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
                Content = new List<InterviewContent>()
            };

            foreach (var categoryId in model.Content.Keys)
            {
                var category = await _categoryRepository.Get(query.OwnerId, categoryId);
                var categoryQuestions = await _questionRepository.GetAllFromPartition(query.OwnerId, categoryId);
                var interviewContent = new InterviewContent
                {
                    Category = category,
                    Questions = new List<Contract.Models.Question>()
                };
                for (var i = 0; i < model.Content[categoryId]; i++)
                {
                    var questionsLeft = categoryQuestions.Where(q => !interviewContent.Questions.Contains(q));
                    if(!questionsLeft.Any())
                        break;
                    var question = questionsLeft.ElementAt(_random.Next(questionsLeft.Count()));
                    interviewContent.Questions.Add(question);
                }
                dto.Content.Add(interviewContent);
            }

            return dto;
        }
    }
}
