using System;

namespace CandidateEvaluator.Contract.Queries.InterviewResult
{
    public class GetInterviewResultQuery : IQuery<Models.InterviewResult>
    {
        public Guid OwnerId { get; }
        public Guid Id { get; }

        public GetInterviewResultQuery(Guid ownerId, Guid id)
        {
            OwnerId = ownerId;
            Id = id;
        }
    }
}
