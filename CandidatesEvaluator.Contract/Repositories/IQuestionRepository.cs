using System;
using CandidatesEvaluator.Contract.Models;

namespace CandidatesEvaluator.Contract.Repositories
{
    public interface IQuestionRepository
    {
        Guid Create(Question model);
        Question Get(Guid categoryId, Guid id);
        void Update(Question model);
        void Delete(Guid id);
    }
}
