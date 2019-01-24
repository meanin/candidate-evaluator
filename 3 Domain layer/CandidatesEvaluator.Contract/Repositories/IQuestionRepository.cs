using CandidateEvaluator.Contract.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CandidateEvaluator.Contract.Repositories
{
    public interface IQuestionRepository
    {
        Task<Guid> Create(Question model);
        Task<Question> Get(Guid ownerId, Guid categoryId, Guid id);
        Task<IEnumerable<Question>> GetAll(Guid ownerId);
        Task<IEnumerable<Question>> GetAllFromCategory(Guid ownerId, Guid categoryId);
        Task<Guid> Update(Question model);
        Task Delete(Guid ownerId, Guid categoryId, Guid questionId);
    }
}
