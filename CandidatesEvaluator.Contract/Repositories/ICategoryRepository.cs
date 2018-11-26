using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Models;

namespace CandidateEvaluator.Contract.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> Add(Category model);
        Task<List<Category>> GetAll();
        Task<Category> Get(Guid id);
        Task Update(Category model);
        Task Delete(Guid id);
    }
}
