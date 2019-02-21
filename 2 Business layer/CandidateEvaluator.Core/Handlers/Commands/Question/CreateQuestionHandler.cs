using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.CoreObjects.Commands.Question;
using CandidateEvaluator.Contract.CoreObjects.Repositories;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Commands.Question
{
    public class CreateQuestionHandler : ICommandHandler<CreateQuestionCommand>
    {
        private readonly IQuestionRepository _modelRepository;
        private readonly IUserRecentActivityRepository _activityRepository;

        public CreateQuestionHandler(IQuestionRepository modelRepository, IUserRecentActivityRepository recentActivityRepository)
        {
            _modelRepository = modelRepository;
            _activityRepository = recentActivityRepository;
        }

        public async Task<Guid> Handle(CreateQuestionCommand command)
        {
            var model = new Contract.CoreObjects.Models.Question
            {
                OwnerId = command.OwnerId,
                CategoryId = command.CategoryId,
                Name = command.Name,
                Text = command.Text,
                Type = command.Type
            };

            var result = await _modelRepository.Create(model);
            await _activityRepository.Upsert(model.OwnerId, new RecentActivity
            {
                Type = EntityType.Question,
                EntityId = result,
                Name = command.Name
            });
            return result;
        }
    }
}
