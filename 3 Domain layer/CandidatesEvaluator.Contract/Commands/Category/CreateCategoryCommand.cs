using System;

namespace CandidateEvaluator.Contract.Commands.Category
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
