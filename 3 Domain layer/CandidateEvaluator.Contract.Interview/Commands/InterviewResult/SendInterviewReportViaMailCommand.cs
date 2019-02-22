using System;
using CandidateEvaluator.Contract.CQRS.Commands;

namespace CandidateEvaluator.Contract.Interview.Commands.InterviewResult
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
