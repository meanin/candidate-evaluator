using System;

namespace CandidateEvaluator.Contract.Queries.InterviewResult
{
    public class GetInterviewResult : IQuery<Models.InterviewResult>
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
    }
}
