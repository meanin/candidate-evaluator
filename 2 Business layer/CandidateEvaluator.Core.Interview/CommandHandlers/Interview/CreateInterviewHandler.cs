using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Account.Models;
using CandidateEvaluator.Contract.Account.Repositories;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Contract.Interview.Commands.Interview;
using CandidateEvaluator.Contract.Interview.Repositories;

namespace CandidateEvaluator.Core.Interview.CommandHandlers.Interview
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
            var model = new Contract.Interview.Models.Interview
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
