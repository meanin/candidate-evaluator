using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Models;

namespace CandidateEvaluator.Contract.Repositories
{
    public interface IInterviewRepository
    {
        Task<Guid> Add(Interview model);
        Task<List<Interview>> GetAll(Guid ownerId);
        Task<Interview> Get(Guid ownerId, Guid id);
        Task<Guid> Update(Interview model);
        Task Delete(Guid ownerId, Guid id);
    }
}
