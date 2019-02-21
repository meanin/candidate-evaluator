using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Commands.Interview;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Commands.Interview
{
    public class CreateInterviewHandler : ICommandHandler<CreateInterviewCommand>
    {
        private readonly IInterviewRepository _modelRepository;
        private readonly IUserRecentActivityRepository _activityRepository;

        public CreateInterviewHandler(IInterviewRepository modelRepository,
            IUserRecentActivityRepository activityRepository)
        {
            _modelRepository = modelRepository;
            _activityRepository = activityRepository;
        }

        public async Task<Guid> Handle(CreateInterviewCommand command)
        {
            var model = new Contract.Models.Interview
            {
                OwnerId = command.OwnerId,
                Name = command.Name,
                Content = new Dictionary<Guid, int>(
                    command.Content.Select(
                        c => new KeyValuePair<Guid, int>(c.CategoryId, c.QuestionCount)))
            };
            var result = await _modelRepository.Add(model);

            await _activityRepository.Upsert(model.OwnerId, new RecentActivity
            {
                Type = EntityType.Interview,
                EntityId = result,
                Name = command.Name
            });
            return result;
        }
    }
}
