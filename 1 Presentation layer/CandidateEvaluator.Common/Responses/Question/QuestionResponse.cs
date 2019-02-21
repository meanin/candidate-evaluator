using System;
using CandidateEvaluator.Common.Types;

namespace CandidateEvaluator.Common.Responses.Question
{
    public class QuestionResponse
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
    }
}
