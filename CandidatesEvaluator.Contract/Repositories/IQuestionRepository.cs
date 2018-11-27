using CandidateEvaluator.Contract.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CandidateEvaluator.Contract.Repositories
{
    public interface IQuestionRepository
    {
        Task<Question> Create(Question model);
        Task<Question> Get(Guid categoryId, Guid id);
        Task<List<Question>> GetAll();
        Task<List<Question>> GetAllFromPartition(Guid partitionKey);
        Task Update(Question model);
        Task Delete(Guid id);
    }
}
