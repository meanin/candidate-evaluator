using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Commands.Question;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Commands.Question
{
    public class CreateQuestionHandler : ICommandHandler<CreateQuestion>
    {
        private readonly IQuestionRepository _modelRepository;
        private readonly IUserRecentActivityRepository _activityRepository;

        public CreateQuestionHandler(IQuestionRepository modelRepository, IUserRecentActivityRepository recentActivityRepository)
        {
            _modelRepository = modelRepository;
            _activityRepository = recentActivityRepository;
        }

        public async Task<Guid> HandleAsync(CreateQuestion command)
        {
            var model = new Contract.Models.Question
            {
                OwnerId = command.OwnerId,
                CategoryId = command.CategoryId,
                Name = command.Name,
                Text = command.Text
            };

            var result = await _modelRepository.Create(model);
            await _activityRepository.Upsert(model.OwnerId, new RecentActivity
            {
                Type = EntityType.Question,
                EntityId = result
            });
            return result;
        }
    }
}
