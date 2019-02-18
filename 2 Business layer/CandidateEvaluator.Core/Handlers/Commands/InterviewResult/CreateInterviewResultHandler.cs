using System;
using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Commands.InterviewResult;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;
using CandidateEvaluator.Contract.Services;

namespace CandidateEvaluator.Core.Handlers.Commands.InterviewResult
{
    public class CreateInterviewResultHandler : ICommandHandler<CreateInterviewResult>
    {
        private readonly IInterviewResultRepository _modelRepository;
        private readonly IMailClient _mailClient;

        public CreateInterviewResultHandler(
            IInterviewResultRepository modelRepository, 
            IMailClient mailClient)
        {
            _modelRepository = modelRepository;
            _mailClient = mailClient;
        }

        public async Task<Guid> Handle(CreateInterviewResult command)
        {
            var model = new Contract.Models.InterviewResult
            {
                OwnerId = command.OwnerId,
                CandidateName = command.CandidateName,
                InterviewDate = command.InterviewDate,
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
            await _mailClient.SendInterviewResultReport(model);
            return result;
        }
    }
}
