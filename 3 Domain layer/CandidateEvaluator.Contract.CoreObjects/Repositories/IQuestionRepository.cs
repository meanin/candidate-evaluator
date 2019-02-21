using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.CoreObjects.Models;

namespace CandidateEvaluator.Contract.CoreObjects.Repositories
{
    public interface IQuestionRepository
    {
        Task<Guid> Create(Question model);
        Task<Question> Get(Guid ownerId, Guid id);
        Task<IEnumerable<Question>> GetAll(Guid ownerId);
        Task<IEnumerable<Question>> GetAllFromCategory(Guid ownerId, Guid categoryId);
        Task<Guid> Update(Question model);
        Task Delete(Guid ownerId, Guid questionId);
    }
}