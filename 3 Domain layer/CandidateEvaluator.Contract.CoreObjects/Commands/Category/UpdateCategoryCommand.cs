using System;
using CandidateEvaluator.Contract.CQRS.Commands;

namespace CandidateEvaluator.Contract.CoreObjects.Commands.Category
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
