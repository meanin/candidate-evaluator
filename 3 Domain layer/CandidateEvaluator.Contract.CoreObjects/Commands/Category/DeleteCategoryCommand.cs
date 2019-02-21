using System;
using CandidateEvaluator.Contract.CQRS.Commands;

namespace CandidateEvaluator.Contract.CoreObjects.Commands.Category
{
    public class DeleteCategoryCommand : ICommand
    {
        public Guid OwnerId { get; }
        public Guid Id { get; }

        public DeleteCategoryCommand(Guid id, Guid ownerId)
        {
            OwnerId = ownerId;
            Id = id;
        }
    }
}
