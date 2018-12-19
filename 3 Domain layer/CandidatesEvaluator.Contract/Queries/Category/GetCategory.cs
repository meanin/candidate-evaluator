using System;

namespace CandidateEvaluator.Contract.Queries.Category
{
    public class GetCategory : IQuery<Models.Category>
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
    }
}
