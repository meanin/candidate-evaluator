using System;
using System.Threading.Tasks;
using CandidatesEvaluator.Contract.Models;

namespace CandidatesEvaluator.Contract.Repositories
{
    public interface ICategoryRepository
    {
        Task<Guid> Add(Category model);
        Task<Category> Get(Guid id);
        Task Update(Category model);
        Task Delete(Guid id);
    }
}
