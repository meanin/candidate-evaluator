using System;

namespace CandidateEvaluator.Contract.Commands.Category
{
    public class DeleteCategory : CommandBase 
    {
        public Guid Id { get; set; }
    }
}
