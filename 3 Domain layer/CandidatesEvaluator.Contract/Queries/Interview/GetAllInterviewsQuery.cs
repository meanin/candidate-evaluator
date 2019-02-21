using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Contract.Queries.Interview
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
