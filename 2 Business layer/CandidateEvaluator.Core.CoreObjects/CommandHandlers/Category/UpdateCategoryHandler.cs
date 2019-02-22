using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Account.Models;
using CandidateEvaluator.Contract.Account.Repositories;
using CandidateEvaluator.Contract.CoreObjects.Commands.Category;
using CandidateEvaluator.Contract.CoreObjects.Repositories;
using CandidateEvaluator.Contract.CQRS.Handlers;

namespace CandidateEvaluator.Core.CoreObjects.CommandHandlers.Category
{
    public class UpdateCategoryHandler : ICommandHandler<UpdateCategoryCommand>
    {
        private readonly ICategoryRepository _modelRepository;
        private readonly IUserRecentActivityRepository _activityRepository;

        public UpdateCategoryHandler(ICategoryRepository modelRepository,
            IUserRecentActivityRepository activityRepository)
        {
            _modelRepository = modelRepository;
            _activityRepository = activityRepository;
        }

        public async Task<Guid> Handle(UpdateCategoryCommand command)
        {
            var model = new Contract.CoreObjects.Models.Category
            {
                Id = command.Id,
                OwnerId = command.OwnerId,
                Name = command.Name
            };
            await _modelRepository.Update(model);

            await _activityRepository.Upsert(model.OwnerId, new RecentActivity
            {
                Type = EntityType.Category,
                EntityId = model.Id,
                Name = command.Name
            });
            return model.Id;
        }
    }
}
