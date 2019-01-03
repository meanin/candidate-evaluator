using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Contract.Queries.InterviewResult
{
    public class GetAllInterviewResults : IQuery<List<Models.InterviewResult>>
    {
        public Guid OwnerId { get; set; }
    }
}
