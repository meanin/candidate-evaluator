using System;

namespace CandidateEvaluator.Contract.Commands.Interview
{
    public class DeleteInterviewCommand : ICommand
    {
        public Guid OwnerId { get; }
        public Guid Id { get; }

        public DeleteInterviewCommand(Guid ownerId, Guid id)
        {
            OwnerId = ownerId;
            Id = id;
        }
    }
}
