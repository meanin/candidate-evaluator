using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Queries.UserActivity;
using CandidateEvaluator.Contract.Repositories;
using CandidateEvaluator.Contract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateEvaluator.Core.Handlers.Queries
{
    public class GetAllUserActivitiesHandler : IQueryHandler<GetAllUserActivities, List<RecentActivity>>
    {
        private readonly IUserRecentActivityRepository _activityRepository;

        public GetAllUserActivitiesHandler(IUserRecentActivityRepository activityRepository)
        {
            this._activityRepository = activityRepository;
        }

        public async Task<List<RecentActivity>> HandleAsync(GetAllUserActivities query)
        {
            var allActivities = await _activityRepository.GetAll(query.OwnerId);
            return allActivities.ToList();
        }
    }
}
