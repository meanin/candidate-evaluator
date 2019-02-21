using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Contract.Queries.Category
{
    public class GetAllCategoriesQuery : IQuery<IEnumerable<Models.Category>>
    {
        public Guid OwnerId { get; }

        public GetAllCategoriesQuery(Guid ownerId)
        {
            OwnerId = ownerId;
        }
    }
}
