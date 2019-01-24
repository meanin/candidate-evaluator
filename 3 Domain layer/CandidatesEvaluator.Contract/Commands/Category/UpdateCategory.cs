using System;

namespace CandidateEvaluator.Contract.Commands.Category
{
    public class UpdateCategory : CommandBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
