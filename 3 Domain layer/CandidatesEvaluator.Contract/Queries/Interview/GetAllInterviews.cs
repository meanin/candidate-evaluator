using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Contract.Queries.Interview
{
    public class GetAllInterviews : IQuery<List<Models.Interview>>
    {
        public Guid OwnerId { get; set; }
    }
}
