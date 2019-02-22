using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CandidateEvaluator.Contract.Interview.Repositories
{
    public interface IInterviewRepository
    {
        Task<Guid> Add(Models.Interview model);
        Task<IEnumerable<Models.Interview>> GetAll(Guid ownerId);
        Task<Models.Interview> Get(Guid ownerId, Guid id);
        Task<Guid> Update(Models.Interview model);
        Task Delete(Guid ownerId, Guid id);
    }
}
