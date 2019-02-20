using System;

namespace CandidateEvaluator.Common.Requests.Question
{
    public class UpdateQuestionRequest
    {
        public Guid CategoryId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public CreateQuestionRequest.QuestionType Type { get; set; }
    }
}
