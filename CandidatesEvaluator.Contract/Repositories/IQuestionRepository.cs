using System;
using System.Threading.Tasks;
using CandidatesEvaluator.Contract.Models;

namespace CandidatesEvaluator.Contract.Repositories
{
    public interface IQuestionRepository
    {
        Task<Guid> Create(Question model);
        Task<Question> Get(Guid categoryId, Guid id);
        Task Update(Question model);
        Task Delete(Guid id);
    }
}
