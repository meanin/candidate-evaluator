using System;

namespace CandidateEvaluator.Contract.Commands.InterviewResult
{
    public class SendInterviewReportViaMailCommand : ICommand
    {
        public Guid OwnerId { get; }
        public Guid Id { get; }

        public SendInterviewReportViaMailCommand(Guid ownerId, Guid id)
        {
            OwnerId = ownerId;
            Id = id;
        }
    }
}
