using System;

namespace CandidateEvaluator.Contract.CoreObjects.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
    }
}
