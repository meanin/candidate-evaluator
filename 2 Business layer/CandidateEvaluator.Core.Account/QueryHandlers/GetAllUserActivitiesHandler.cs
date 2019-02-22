using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Account.Models;
using CandidateEvaluator.Contract.Account.Queries;
using CandidateEvaluator.Contract.Account.Repositories;
using CandidateEvaluator.Contract.CQRS.Handlers;

namespace CandidateEvaluator.Core.Account.QueryHandlers
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
