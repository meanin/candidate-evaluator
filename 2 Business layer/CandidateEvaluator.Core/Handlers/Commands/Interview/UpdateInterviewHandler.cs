﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Commands.Interview;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Commands.Interview
{
    public class UpdateInterviewHandler : ICommandHandler<UpdateInterview>
    {
        private readonly IInterviewRepository _modelRepository;
        private readonly IUserRecentActivityRepository _activityRepository;

        public UpdateInterviewHandler(IInterviewRepository modelRepository,
            IUserRecentActivityRepository activityRepository)
        {
            _modelRepository = modelRepository;
            _activityRepository = activityRepository;
        }

        public async Task<Guid> HandleAsync(UpdateInterview command)
        {
            var model = new Contract.Models.Interview
            {
                Id = command.Id,
                OwnerId = command.OwnerId,
                Name = command.Name
            };
            await _modelRepository.Update(model);

            await _activityRepository.Upsert(model.OwnerId, new RecentActivity
            {
                Type = EntityType.Interview,
                EntityId = model.Id
            });
            return model.Id;
        }
    }
}
