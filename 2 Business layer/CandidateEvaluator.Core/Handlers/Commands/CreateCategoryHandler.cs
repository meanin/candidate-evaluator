using CandidateEvaluator.Contract.Commands.Category;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;
using CandidateEvaluator.Contract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateEvaluator.Core.Handlers.Commands
{
    public class CreateCategoryHandler : ICommandHandler<CreateCategory>
    {
        private readonly ICategoryRepository _modelRepository;
        private readonly IUserRecentActivityRepository _activityRepository;

        public CreateCategoryHandler(ICategoryRepository modelRepository,
            IUserRecentActivityRepository activityRepository)
        {
            _modelRepository = modelRepository;
            _activityRepository = activityRepository;
        }

        public async Task<Guid> HandleAsync(CreateCategory command)
        {
            var model = new Category
            {
                OwnerId = command.OwnerId,
                Name = command.Name
            };

            var result = await _modelRepository.Add(model);
            await _activityRepository.Upsert(model.OwnerId, new RecentActivity
            {
                Type = EntityType.Category,
                EntityId = result
            });
            return result;
        }
    }
}
