using System;

namespace CandidatesEvaluator.Contract.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
