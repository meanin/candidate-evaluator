using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Contract.Queries.Category
{
    public class GetAllCategories : IQuery<List<Models.Category>>
    {
        public Guid OwnerId { get; set; }
    }
}
