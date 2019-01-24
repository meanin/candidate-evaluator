using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Models;

namespace CandidateEvaluator.Contract.Repositories
{
    public interface ICategoryRepository
    {
        Task<Guid> Add(Category model);
        Task<IEnumerable<Category>> GetAll(Guid ownerId);
        Task<Category> Get(Guid ownerId, Guid id);
        Task<Guid> Update(Category model);
        Task Delete(Guid ownerId, Guid id);
    }
}
