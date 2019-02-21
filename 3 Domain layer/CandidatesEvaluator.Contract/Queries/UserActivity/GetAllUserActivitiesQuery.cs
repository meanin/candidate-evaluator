using CandidateEvaluator.Contract.Models;
using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Contract.Queries.UserActivity
{
    public class GetAllUserActivitiesQuery : IQuery<IEnumerable<RecentActivity>>
    {
        public Guid OwnerId { get; }

        public GetAllUserActivitiesQuery(Guid ownerId)
        {
            OwnerId = ownerId;
        }
    }
}
