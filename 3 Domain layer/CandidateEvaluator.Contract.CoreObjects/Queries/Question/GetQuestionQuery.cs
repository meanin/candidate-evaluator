using System;
using CandidateEvaluator.Contract.CQRS.Queries;

namespace CandidateEvaluator.Contract.CoreObjects.Queries.Question
{
    public class GetQuestionQuery : IQuery<Models.Question>
    {
        public Guid OwnerId { get; }
        public Guid Id { get; }

        public GetQuestionQuery(Guid ownerId, Guid id)
        {
            OwnerId = ownerId;
            Id = id;
        }
    }
}
