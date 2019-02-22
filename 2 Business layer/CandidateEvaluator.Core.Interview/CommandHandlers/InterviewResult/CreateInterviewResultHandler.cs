using System;
using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Contract.Interview.Commands.InterviewResult;
using CandidateEvaluator.Contract.Interview.Models;
using CandidateEvaluator.Contract.Interview.Repositories;
using CandidateEvaluator.Contract.Interview.Services;

namespace CandidateEvaluator.Core.Interview.CommandHandlers.InterviewResult
{
    public class CreateInterviewResultHandler : ICommandHandler<CreateInterviewResultCommand>
    {
        private readonly IInterviewResultRepository _modelRepository;
        private readonly IMailService _mailService;

        public CreateInterviewResultHandler(
            IInterviewResultRepository modelRepository, 
            IMailService mailService)
        {
            _modelRepository = modelRepository;
            _mailService = mailService;
        }

        public async Task<Guid> Handle(CreateInterviewResultCommand command)
        {
            var model = new Contract.Interview.Models.InterviewResult
            {
                OwnerId = command.OwnerId,
                CandidateName = command.CandidateName,
                ReviewerName = command.ReviewerName,
                InterviewDate = command.InterviewDate,
                InterviewTemplateName = command.InterviewTemplateName,
                Content = command.Content.Select(c => new CategoryResult
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    QuestionResults = c.QuestionResults.Select(q => new QuestionResult
                    {
                        QuestionId = q.QuestionId,
                        QuestionName = q.QuestionName,
                        Score = q.Score,
                        Notes = q.Notes
                    }).ToList()
                }).ToList()
            };

            var result = await _modelRepository.Add(model);

            await _mailService.SendInterviewResultReport(model, command.OwnerId);
            return result;
        }
    }
}
