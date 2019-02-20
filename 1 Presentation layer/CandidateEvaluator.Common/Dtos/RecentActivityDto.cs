using System;

namespace CandidateEvaluator.Common.Dtos
{
    public class RecentActivityDto
    {
        public string Type { get; set; }
        public Guid EntityId { get; set; }
        public string Name { get; set; }
    }
}
