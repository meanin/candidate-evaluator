using System;

namespace CandidateEvaluator.Common.Requests.Category
{
    public class UpdateCategoryRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
