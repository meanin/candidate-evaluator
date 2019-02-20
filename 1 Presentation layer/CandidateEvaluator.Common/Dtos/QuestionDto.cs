using System;
using CandidateEvaluator.Common.Requests.Question;

namespace CandidateEvaluator.Common.Dtos
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public CreateQuestionRequest.QuestionType Type { get; set; }
    }
}
