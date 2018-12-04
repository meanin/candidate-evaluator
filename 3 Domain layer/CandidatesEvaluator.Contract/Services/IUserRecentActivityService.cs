using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Models;

namespace CandidateEvaluator.Contract.Services
{
    public interface IUserRecentActivityService
    {
        Task<IEnumerable<RecentActivity>> GetAll(Guid ownerId);
    }
}
