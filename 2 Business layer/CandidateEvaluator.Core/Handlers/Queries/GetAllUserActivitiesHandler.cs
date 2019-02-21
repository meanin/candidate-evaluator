using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Queries.UserActivity;
using CandidateEvaluator.Contract.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.CQRS.Handlers;

namespace CandidateEvaluator.Core.Handlers.Queries
{
    public class GetAllUserActivitiesHandler : IQueryHandler<GetAllUserActivitiesQuery, IEnumerable<RecentActivity>>
    {
        private readonly IUserRecentActivityRepository _activityRepository;

        public GetAllUserActivitiesHandler(IUserRecentActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public Task<IEnumerable<RecentActivity>> Handle(GetAllUserActivitiesQuery query)
        {
            return _activityRepository.GetAll(query.OwnerId);
        }
    }
}
