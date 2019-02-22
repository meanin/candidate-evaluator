using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Contract.Interview.Commands.InterviewResult;
using CandidateEvaluator.Contract.Interview.Repositories;
using CandidateEvaluator.Contract.Interview.Services;

namespace CandidateEvaluator.Core.Interview.CommandHandlers.InterviewResult
{
    public class SendInterviewReportViaMailHandler : ICommandHandler<SendInterviewReportViaMailCommand>
    {
        private readonly IInterviewResultRepository _modelRepository;
        private readonly IMailService _mailService;

        public SendInterviewReportViaMailHandler(
            IInterviewResultRepository modelRepository, 
            IMailService mailService)
        {
            _modelRepository = modelRepository;
            _mailService = mailService;
        }

        public async Task<Guid> Handle(SendInterviewReportViaMailCommand command)
        {
            var model = await _modelRepository.Get(command.OwnerId, command.Id);
            await _mailService.SendInterviewResultReport(model, command.OwnerId);
            return Guid.Empty;
        }
    }
}
