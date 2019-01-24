using CandidateEvaluator.Contract.Models;
using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Contract.Queries.UserActivity
{
    public class GetAllUserActivities : IQuery<IEnumerable<RecentActivity>>
    {
        public Guid OwnerId { get; set; }
    }
}
