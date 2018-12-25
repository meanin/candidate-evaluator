using System;
using CandidateEvaluator.Contract.Dtos;

namespace CandidateEvaluator.Contract.Queries.Interview
{
    public class GetInterview : IQuery<InterviewDto>
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
    }
}
