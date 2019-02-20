using System;

namespace CandidateEvaluator.Contract.Commands.Category
{
    public class UpdateCategoryCommand : ICommand
    {
        public Guid OwnerId { get; }
        public Guid Id { get; }
        public string Name { get; }

        public UpdateCategoryCommand(Guid ownerId, Guid id, string name)
        {
            OwnerId = ownerId;
            Id = id;
            Name = name;
        }
    }
}
