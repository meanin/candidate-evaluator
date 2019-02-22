using System;
using System.Collections.Generic;
using CandidateEvaluator.Contract.Account.Models;
using CandidateEvaluator.Contract.CQRS.Queries;

namespace CandidateEvaluator.Contract.Account.Queries
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
