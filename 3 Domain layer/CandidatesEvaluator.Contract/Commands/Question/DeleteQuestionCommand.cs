using System;

namespace CandidateEvaluator.Contract.Commands.Question
{
    public class DeleteQuestionCommand : ICommand
    {
        public Guid OwnerId { get; }
        public Guid Id { get; }

        public DeleteQuestionCommand(Guid ownerId, Guid id)
        {
            OwnerId = ownerId;
            Id = id;
        }
    }
}
