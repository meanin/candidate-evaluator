using CandidateEvaluator.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateEvaluator.Contract.Queries.UserActivity
{
    public class GetAllUserActivities : IQuery<List<RecentActivity>>
    {
        public Guid OwnerId { get; set; }
    }
}
