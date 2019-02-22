using System;
using CandidateEvaluator.Contract.CQRS.Commands;

namespace CandidateEvaluator.Contract.Interview.Commands.InterviewResult
{
    public class DeleteInterviewResultCommand : ICommand
    {
        public Guid OwnerId { get; }
        public Guid Id { get; }

        public DeleteInterviewResultCommand(Guid ownerId, Guid id)
        {
            OwnerId = ownerId;
            Id = id;
        }
    }
}
