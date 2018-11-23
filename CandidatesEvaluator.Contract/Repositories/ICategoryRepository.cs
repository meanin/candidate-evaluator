using System;
using CandidatesEvaluator.Contract.Models;

namespace CandidatesEvaluator.Contract.Repositories
{
    public interface ICategoryRepository
    {
        Guid Create(Category model);
        Category Get(Guid id);
        void Update(Category model);
        void Delete(Guid id);
    }
}
