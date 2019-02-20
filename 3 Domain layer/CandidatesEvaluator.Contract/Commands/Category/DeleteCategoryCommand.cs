using System;

namespace CandidateEvaluator.Contract.Commands.Category
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
