using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.CoreObjects.Commands.Question;
using CandidateEvaluator.Contract.CoreObjects.Repositories;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.CoreObjects.CommandHandlers.Question
{
    public class DeleteQuestionHandler : ICommandHandler<DeleteQuestionCommand>
    {
        private readonly IQuestionRepository _modelRepository;
        private readonly IUserRecentActivityRepository _activityRepository;

        public DeleteQuestionHandler(IQuestionRepository questionRepository, IUserRecentActivityRepository activityRepository)
        {
            _modelRepository = questionRepository;
            _activityRepository = activityRepository;
        }

        public async Task<Guid> Handle(DeleteQuestionCommand command)
        {
            await _modelRepository.Delete(command.OwnerId, command.Id);
            await _activityRepository.Delete(command.OwnerId, new RecentActivity
            {
                Type = EntityType.Question,
                EntityId = command.Id
            });

            return Guid.Empty;
        }
    }
}
