using System;
using CandidateEvaluator.Contract.CQRS.Queries;

namespace CandidateEvaluator.Contract.Interview.Queries.Interview
{
    public class GetInterviewQuery : IQuery<Models.Interview>
    {
        public GetInterviewQuery(Guid ownerId, Guid id)
        {
            Id = id;
            OwnerId = ownerId;
        }

        public Guid Id { get; }
        public Guid OwnerId { get; }
    }
}
