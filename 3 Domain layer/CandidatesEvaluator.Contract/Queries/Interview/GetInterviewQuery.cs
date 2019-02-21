using System;
using CandidateEvaluator.Contract.Dtos;

namespace CandidateEvaluator.Contract.Queries.Interview
{
    public class GetInterviewQuery : IQuery<InterviewDto>
    {
        public GetInterviewQuery(Guid ownerId, Guid id)
        {
            Id = id;
            OwnerId = ownerId;
        }

        public Guid Id { get; }
        public Guid OwnerId { get; }
    }
}
