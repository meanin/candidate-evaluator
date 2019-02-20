using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Commands.Category;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Commands.Category
{
    public class CreateCategoryHandler : ICommandHandler<CreateCategoryCommand>
    {
        private readonly ICategoryRepository _modelRepository;
        private readonly IUserRecentActivityRepository _activityRepository;

        public CreateCategoryHandler(ICategoryRepository modelRepository,
            IUserRecentActivityRepository activityRepository)
        {
            _modelRepository = modelRepository;
            _activityRepository = activityRepository;
        }

        public async Task<Guid> Handle(CreateCategoryCommand command)
        {
            var model = new Contract.Models.Category
            {
                OwnerId = command.OwnerId,
                Name = command.Name
            };

            var result = await _modelRepository.Add(model);
            await _activityRepository.Upsert(model.OwnerId, new RecentActivity
            {
                Type = EntityType.Category,
                EntityId = result,
                Name = command.Name
            });
            return result;
        }
    }
}
