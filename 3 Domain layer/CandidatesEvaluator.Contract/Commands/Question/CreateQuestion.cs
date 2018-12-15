using System;

namespace CandidateEvaluator.Contract.Commands.Question
{
    public class CreateQuestion : CommandBase
    {
        public string Name { get; set; }

        public string Text { get; set; }

        public Guid CategoryId { get; set; }
    }
}
