using System;

namespace CandidateEvaluator.Contract.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
    }
}
