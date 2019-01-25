using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Commands.Question;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Commands.Question
{
    public class UpdateQuestionHandler : ICommandHandler<UpdateQuestion>
    {
        private readonly IQuestionRepository _modelRepository;
        private readonly IUserRecentActivityRepository _activityRepository;

        public UpdateQuestionHandler(IQuestionRepository modelRepository, IUserRecentActivityRepository activityRepository)
        {
            _modelRepository = modelRepository;
            _activityRepository = activityRepository;
        }

        public async Task<Guid> Handle(UpdateQuestion command)
        {
            var model = new Contract.Models.Question
            {
                CategoryId = command.CategoryId,
                Name = command.Name,
                Text = command.Text,
                Id = command.Id,
                OwnerId = command.OwnerId,
                Type = command.Type
            };
            await _modelRepository.Update(model);
            await _activityRepository.Upsert(model.OwnerId, new RecentActivity
            {
                Type = EntityType.Question,
                EntityId = model.Id,
                Name = command.Name
            });

            return model.Id;
        }
    }
}
