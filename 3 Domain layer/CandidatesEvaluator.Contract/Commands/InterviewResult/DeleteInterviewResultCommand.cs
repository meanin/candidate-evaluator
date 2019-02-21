using System;

namespace CandidateEvaluator.Contract.Commands.InterviewResult
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
