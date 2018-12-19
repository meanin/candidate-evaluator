using System;

namespace CandidateEvaluator.Contract.Commands.Interview
{
    public class DeleteInterview : CommandBase
    {
        public Guid Id { get; set; }
    }
}
