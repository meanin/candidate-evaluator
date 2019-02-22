using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Account.Models;
using CandidateEvaluator.Contract.Account.Repositories;
using CandidateEvaluator.Contract.CoreObjects.Commands.Category;
using CandidateEvaluator.Contract.CoreObjects.Repositories;
using CandidateEvaluator.Contract.CQRS.Handlers;

namespace CandidateEvaluator.Core.CoreObjects.CommandHandlers.Category
{
    public class DeleteCategoryHandler : ICommandHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _modelRepository;
        private readonly IUserRecentActivityRepository _activityRepository;

        public DeleteCategoryHandler(ICategoryRepository modelRepository,
            IUserRecentActivityRepository activityRepository)
        {
            _modelRepository = modelRepository;
            _activityRepository = activityRepository;
        }

        public async Task<Guid> Handle(DeleteCategoryCommand command)
        {
            await _modelRepository.Delete(command.OwnerId, command.Id);
            await _activityRepository.Delete(command.OwnerId, new RecentActivity
            {
                Type = EntityType.Category,
                EntityId = command.Id
            });
            return Guid.Empty;
        }
    }
}
