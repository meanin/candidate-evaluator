using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Queries.UserActivity;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.QueryHandlers
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
