using System;

namespace CandidateEvaluator.Contract.Commands.Question
{
    public class DeleteQuestion : CommandBase
    {
        public Guid CategoryId { get; set; }

        public Guid Id { get; set; }
    }
}
