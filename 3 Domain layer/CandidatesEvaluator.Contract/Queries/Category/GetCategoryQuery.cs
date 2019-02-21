using System;

namespace CandidateEvaluator.Contract.Queries.Category
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
