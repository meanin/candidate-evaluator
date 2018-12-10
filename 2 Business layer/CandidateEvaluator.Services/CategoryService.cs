using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;
using CandidateEvaluator.Contract.Services;

namespace CandidateEvaluator.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _modelRepository;
        private readonly IUserRecentActivityRepository _activityRepository;

        private static EntityType ModelType = EntityType.Category;

        public CategoryService(
            ICategoryRepository modelRepository, 
            IUserRecentActivityRepository activityRepository)
        {
            _modelRepository = modelRepository;
            _activityRepository = activityRepository;
        }

        public async Task<Guid> AddAsync(Category model)
        {
            var result = await _modelRepository.Add(model);
            await _activityRepository.Upsert(model.OwnerId, new RecentActivity
            {
                Type = ModelType,
                EntityId = result
            });
            return result;
        }

        public Task<List<Category>> GetAll(Guid ownerId)
        {
            return _modelRepository.GetAll(ownerId);
        }

        public Task<Category> Get(Guid ownerId, Guid id)
        {
            return _modelRepository.Get(ownerId, id);
        }

        public async Task<Guid> Update(Category model)
        {
            await _modelRepository.Update(model);
            await _activityRepository.Upsert(model.OwnerId, new RecentActivity
            {
                Type = ModelType,
                EntityId = model.Id
            });
            return model.Id;
        }

        public async Task Delete(Guid ownerId, Guid id)
        {
            await _modelRepository.Delete(ownerId, id);
            await _activityRepository.Delete(ownerId, new RecentActivity
            {
                Type = ModelType,
                EntityId = id
            });
        }
    }
}
