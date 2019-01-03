using System;

namespace CandidateEvaluator.Contract.Commands.InterviewResult
{
    public class DeleteInterviewResult : CommandBase
    {
        public Guid Id { get; set; }
    }
}
