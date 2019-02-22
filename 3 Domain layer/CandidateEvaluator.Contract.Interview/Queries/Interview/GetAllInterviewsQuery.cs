using System;
using System.Collections.Generic;
using CandidateEvaluator.Contract.CQRS.Queries;

namespace CandidateEvaluator.Contract.Interview.Queries.Interview
{
    public class GetAllInterviewsQuery : IQuery<List<(Guid Id, string Name)>>
    {
        public Guid OwnerId { get; }

        public GetAllInterviewsQuery(Guid ownerId)
        {
            OwnerId = ownerId;
        }
    }
}
