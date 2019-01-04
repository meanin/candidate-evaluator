﻿using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Commands.Interview;
using CandidateEvaluator.Contract.Commands.InterviewResult;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Commands.InterviewResult
{
    public class DeleteInterviewResultHandler : ICommandHandler<DeleteInterviewResult>
    {
        private readonly IInterviewResultRepository _modelRepository;

        public DeleteInterviewResultHandler(IInterviewResultRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task<Guid> Handle(DeleteInterviewResult command)
        {
            await _modelRepository.Delete(command.OwnerId, command.Id);
            return Guid.Empty;
        }
    }
}