using System;
using CandidateEvaluator.Contract.CQRS.Queries;

namespace CandidateEvaluator.Contract.CoreObjects.Queries.Category
{
    public class GetCategoryQuery : IQuery<Models.Category>
    {
        public Guid Id { get; }
        public Guid OwnerId { get; }

        public GetCategoryQuery(Guid ownerId, Guid id)
        {
            Id = id;
            OwnerId = ownerId;
        }
    }
}
