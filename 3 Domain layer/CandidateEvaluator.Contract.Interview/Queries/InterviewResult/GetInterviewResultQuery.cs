using System;
using CandidateEvaluator.Contract.CQRS.Queries;

namespace CandidateEvaluator.Contract.Interview.Queries.InterviewResult
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
