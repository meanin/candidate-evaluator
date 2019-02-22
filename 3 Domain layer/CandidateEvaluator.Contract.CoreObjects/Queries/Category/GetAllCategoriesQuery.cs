using System;
using System.Collections.Generic;
using CandidateEvaluator.Contract.CQRS.Queries;

namespace CandidateEvaluator.Contract.CoreObjects.Queries.Category
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
