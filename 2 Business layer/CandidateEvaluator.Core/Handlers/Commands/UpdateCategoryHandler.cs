using CandidateEvaluator.Contract.Commands.Category;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;
using CandidateEvaluator.Contract.Services;
using System;
using System.Threading.Tasks;

namespace CandidateEvaluator.Core.Handlers.Commands
{
    public class UpdateCategoryHandler : ICommandHandler<UpdateCategory>
    {
        private readonly ICategoryRepository _modelRepository;
        private readonly IUserRecentActivityRepository _activityRepository;

        public UpdateCategoryHandler(ICategoryRepository modelRepository,
            IUserRecentActivityRepository activityRepository)
        {
            _modelRepository = modelRepository;
            _activityRepository = activityRepository;
        }

        public async Task<Guid> HandleAsync(UpdateCategory command)
        {
            var model = new Category
            {
                Id = command.Id,
                OwnerId = command.OwnerId,
                Name = command.Name
            };

            await _modelRepository.Update(model);
            await _activityRepository.Upsert(model.OwnerId, new RecentActivity
            {
                Type = EntityType.Category,
                EntityId = model.Id
            });
            return model.Id;
        }
    }
}
