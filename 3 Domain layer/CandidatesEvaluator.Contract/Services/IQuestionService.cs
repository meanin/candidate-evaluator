using CandidateEvaluator.Contract.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CandidateEvaluator.Contract.Services
{
    public interface IQuestionService
    {
        Task<Question> Add(Question question);

        Task<List<Question>> GetAll(Guid ownerId);

        Task<List<Question>> GetAllFromCategory(Guid ownerId, Guid categoryId);

        Task<Question> Get(Guid ownerId, Guid categoryId, Guid questionId);

        Task Update(Question model);

        Task Delete(Guid ownerId, Guid categoryId, Guid questionId);
    }
}
