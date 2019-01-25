using System;
using CandidateEvaluator.Contract.Models;

namespace CandidateEvaluator.Contract.Commands.Question
{
    public class CreateQuestion : CommandBase
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public Guid CategoryId { get; set; }
        public QuestionType Type { get; set; }
    }
}
