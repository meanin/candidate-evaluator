using System;
using CandidateEvaluator.Contract.Dtos;

namespace CandidateEvaluator.Contract.Queries.Interview
{
    public class GetAllInterviews : IQuery<InterviewListDto>
    {
        public Guid OwnerId { get; set; }
    }
}
