using System;

namespace CandidateEvaluator.Common.Responses.Dashboard
{
    public class RecentActivityResponse
    {
        public string Type { get; set; }
        public Guid EntityId { get; set; }
        public string Name { get; set; }
    }
}
