using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;
using CandidateEvaluator.Contract.Services;

namespace CandidateEvaluator.Services
{
    public class UserRecentActivityService : IUserRecentActivityService
    {
        private readonly IUserRecentActivityRepository _activityRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IQuestionRepository _questionRepository;

        public UserRecentActivityService(IUserRecentActivityRepository activityRepository, ICategoryRepository categoryRepository, IQuestionRepository questionRepository)
        {
            _activityRepository = activityRepository;
            _categoryRepository = categoryRepository;
            _questionRepository = questionRepository;
        }

        public async Task<IEnumerable<RecentActivity>> GetAll(Guid ownerId)
        {
            var activities = new List<RecentActivity>();
            foreach (var activity in await _activityRepository.GetAll(ownerId))
            {
                activities.Add(await FetchActivity(ownerId, activity));
            }

            return activities;
        }

        private async Task<RecentActivity> FetchActivity(Guid ownerId, RecentActivity activity)
        {
            var fetched = new RecentActivity
            {
                EntityId = activity.EntityId,
                Type = activity.Type
            };

            switch (activity.Type)
            {
                case EntityType.Category:
                    var category = await _categoryRepository.Get(ownerId, activity.EntityId);
                    fetched.Name = category.Name;
                    break;

                case EntityType.Question:
                    break;

                default:
                    break;
            }

            return fetched;
        }
    }
}
