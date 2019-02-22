using System;
using System.Collections.Generic;
using CandidateEvaluator.Contract.CQRS.Queries;

namespace CandidateEvaluator.Contract.Interview.Queries.InterviewResult
{
    public class GetAllInterviewResultsQuery : IQuery<IEnumerable<Models.InterviewResult>>
    {
        public Guid OwnerId { get; }

        public GetAllInterviewResultsQuery(Guid ownerId)
        {
            OwnerId = ownerId;
        }
    }
}
