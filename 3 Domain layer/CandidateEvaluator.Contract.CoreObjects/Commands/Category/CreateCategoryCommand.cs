using System;
using CandidateEvaluator.Contract.CQRS.Commands;

namespace CandidateEvaluator.Contract.CoreObjects.Commands.Category
{
    public class CreateCategoryCommand : ICommand
    {
        public Guid OwnerId { get; }
        public string Name { get; }

        public CreateCategoryCommand(Guid ownerId, string name)
        {
            OwnerId = ownerId;
            Name = name;
        }
    }
}
