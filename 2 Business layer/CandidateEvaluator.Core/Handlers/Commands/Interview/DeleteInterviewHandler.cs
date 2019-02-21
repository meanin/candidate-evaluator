using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Commands.Interview;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Commands.Interview
{
    public class DeleteInterviewHandler : ICommandHandler<DeleteInterviewCommand>
    {
        private readonly IInterviewRepository _modelRepository;
        private readonly IUserRecentActivityRepository _activityRepository;

        public DeleteInterviewHandler(IInterviewRepository modelRepository,
            IUserRecentActivityRepository activityRepository)
        {
            _modelRepository = modelRepository;
            _activityRepository = activityRepository;
        }

        public async Task<Guid> Handle(DeleteInterviewCommand command)
        {
            await _modelRepository.Delete(command.OwnerId, command.Id);
            await _activityRepository.Delete(command.OwnerId, new RecentActivity
            {
                Type = EntityType.Interview,
                EntityId = command.Id
            });
            return Guid.Empty;
        }
    }
}
