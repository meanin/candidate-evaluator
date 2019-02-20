using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Contract.Queries.Interview
{
    public class GetAllInterviews : IQuery<List<(Guid Id, string Name)>>
    {
        public Guid OwnerId { get; set; }
    }
}
