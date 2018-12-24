﻿using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Commands.Question;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Commands.Question
{
    public class DeleteQuestionHandler : ICommandHandler<DeleteQuestion>
    {
        private readonly IQuestionRepository _modelRepository;
        private readonly IUserRecentActivityRepository _activityRepository;

        public DeleteQuestionHandler(IQuestionRepository questionRepository, IUserRecentActivityRepository activityRepository)
        {
            _modelRepository = questionRepository;
            _activityRepository = activityRepository;
        }

        public async Task<Guid> HandleAsync(DeleteQuestion command)
        {
            await _modelRepository.Delete(command.OwnerId, command.CategoryId, command.Id);
            await _activityRepository.Delete(command.OwnerId, new RecentActivity
            {
                Type = EntityType.Question,
                EntityId = command.Id
            });

            return Guid.Empty;
        }
    }
}