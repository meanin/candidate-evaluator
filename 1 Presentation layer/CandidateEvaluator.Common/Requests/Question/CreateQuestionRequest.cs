using System;
using CandidateEvaluator.Common.Types;

namespace CandidateEvaluator.Common.Requests.Question
{
    public partial class CreateQuestionRequest
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public Guid CategoryId { get; set; }
        public QuestionType Type { get; set; }
    }
}
