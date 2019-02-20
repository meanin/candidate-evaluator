using System;

namespace CandidateEvaluator.Common.Requests.Question
{
    public class CreateQuestionRequest
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public Guid CategoryId { get; set; }
        public QuestionType Type { get; set; }

        public enum QuestionType
        {
            Regular,
            Snippet
        }
    }
}
