using System;

namespace CandidateEvaluator.Contract.Commands.Question
{
    public class DeleteQuestion : CommandBase
    {

        public Guid Id { get; set; }
    }
}
