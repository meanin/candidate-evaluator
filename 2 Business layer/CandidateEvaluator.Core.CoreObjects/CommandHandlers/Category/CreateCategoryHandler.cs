using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.CoreObjects.Commands.Category;
using CandidateEvaluator.Contract.CoreObjects.Repositories;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.CoreObjects.CommandHandlers.Category
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
            var model = new Contract.CoreObjects.Models.Category
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
