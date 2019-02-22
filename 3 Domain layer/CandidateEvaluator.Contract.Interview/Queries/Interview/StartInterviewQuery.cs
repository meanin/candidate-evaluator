using System;
using CandidateEvaluator.Contract.CQRS.Queries;
using CandidateEvaluator.Contract.Interview.Models;

namespace CandidateEvaluator.Contract.Interview.Queries.Interview
{
    public class StartInterviewQuery : IQuery<StartInterview>
    {
        public StartInterviewQuery(Guid ownerId, Guid id)
        {
            Id = id;
            OwnerId = ownerId;
        }

        public Guid Id { get; }
        public Guid OwnerId { get; }
    }
}
