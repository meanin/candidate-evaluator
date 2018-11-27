using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CandidateEvaluator.Data.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        public Task<Guid> Create(Question model)
        {
            throw new NotImplementedException();
        }

        public Task<Question> Get(Guid categoryId, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Question model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Question>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Question>> GetAllFromPartition(Guid partitionKey)
        {
            throw new NotImplementedException();
        }
    }
}
