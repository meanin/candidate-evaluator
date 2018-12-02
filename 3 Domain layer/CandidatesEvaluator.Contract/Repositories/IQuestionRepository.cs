using CandidateEvaluator.Contract.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CandidateEvaluator.Contract.Repositories
{
    public interface IQuestionRepository
    {
        Task<Question> Create(Question model);
        Task<Question> Get(Guid ownerId, Guid categoryId, Guid id);
        Task<List<Question>> GetAll(Guid ownerId);
        Task<List<Question>> GetAllFromPartition(Guid ownerId, Guid categoryId);
        Task Update(Question model);
        Task Delete(Guid ownerId, Guid categoryId, Guid questionId);
    }
}
