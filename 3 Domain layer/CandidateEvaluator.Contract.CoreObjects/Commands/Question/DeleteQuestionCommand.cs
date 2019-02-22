using System;
using CandidateEvaluator.Contract.CQRS.Commands;

namespace CandidateEvaluator.Contract.CoreObjects.Commands.Question
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
