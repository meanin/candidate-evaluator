using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Commands.Category;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Commands.Category
{
    public class DeleteCategoryHandler : ICommandHandler<DeleteCategory>
    {
        private readonly ICategoryRepository _modelRepository;
        private readonly IUserRecentActivityRepository _activityRepository;

        public DeleteCategoryHandler(ICategoryRepository modelRepository,
            IUserRecentActivityRepository activityRepository)
        {
            _modelRepository = modelRepository;
            _activityRepository = activityRepository;
        }

        public async Task<Guid> Handle(DeleteCategory command)
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
