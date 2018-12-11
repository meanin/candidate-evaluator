using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Queries.UserActivity;
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
        private readonly IUserRecentActivityService _activityService;

        public GetAllUserActivitiesHandler(IUserRecentActivityService activityService)
        {
            _activityService = activityService;
        }

        public async Task<List<RecentActivity>> HandleAsync(GetAllUserActivities query)
        {
            var allActivities = await _activityService.GetAll(query.OwnerId);
            return allActivities.ToList();
        }
    }
}
