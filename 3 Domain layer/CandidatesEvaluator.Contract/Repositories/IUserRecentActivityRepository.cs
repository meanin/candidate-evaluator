﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Models;

namespace CandidateEvaluator.Contract.Repositories
{
    public interface IUserRecentActivityRepository
    {
        Task<IEnumerable<RecentActivity>> GetAll(Guid ownerId);
        Task Upsert(Guid ownerId, RecentActivity userActivity);
        Task Delete(Guid ownerId, RecentActivity userActivity);
    }
}
