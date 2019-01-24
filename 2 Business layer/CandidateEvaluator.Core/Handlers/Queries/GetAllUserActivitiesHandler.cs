using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Queries.UserActivity;
using CandidateEvaluator.Contract.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CandidateEvaluator.Core.Handlers.Queries
{
    public class GetAllUserActivitiesHandler : IQueryHandler<GetAllUserActivities, IEnumerable<RecentActivity>>
    {
        private readonly IUserRecentActivityRepository _activityRepository;

        public GetAllUserActivitiesHandler(IUserRecentActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public Task<IEnumerable<RecentActivity>> Handle(GetAllUserActivities query)
        {
            return _activityRepository.GetAll(query.OwnerId);
        }
    }
}
