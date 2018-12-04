using System;

namespace CandidateEvaluator.Contract.Models
{
    public class RecentActivity
    {
        public EntityType Type { get; set; }
        public Guid EntityId { get; set; }
        public string Name { get; set; }
    }
}
