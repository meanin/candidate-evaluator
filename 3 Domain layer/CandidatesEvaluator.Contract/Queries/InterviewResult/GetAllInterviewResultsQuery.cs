using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Contract.Queries.InterviewResult
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
