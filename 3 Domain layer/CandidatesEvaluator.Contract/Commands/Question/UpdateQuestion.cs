using System;

namespace CandidateEvaluator.Contract.Commands.Question
{
    public class UpdateQuestion : CommandBase
    {
        public Guid CategoryId { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }
    }
}
