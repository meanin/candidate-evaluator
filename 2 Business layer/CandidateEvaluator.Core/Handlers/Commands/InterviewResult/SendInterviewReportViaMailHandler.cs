using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Commands.InterviewResult;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Contract.Repositories;
using CandidateEvaluator.Contract.Services;

namespace CandidateEvaluator.Core.Handlers.Commands.InterviewResult
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
