﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Models;

namespace CandidateEvaluator.Contract.Repositories
{
    public interface IInterviewResultRepository
    {
        Task<Guid> Add(InterviewResult model);
        Task<IEnumerable<InterviewResult>> GetAll(Guid ownerId);
        Task<InterviewResult> Get(Guid ownerId, Guid id);
        Task Delete(Guid ownerId, Guid id);
    }
}
