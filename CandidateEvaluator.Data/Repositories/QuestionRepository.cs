using CandidateEvaluator.Contract.Configuration;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;
using CandidateEvaluator.Data.Entities;
using CandidateEvaluator.Data.Wrappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CandidateEvaluator.Data.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private AzureTableStorageWrapper<QuestionEntity> _table;

        public QuestionRepository(AzureTableStorageOptions options)
        {
            _table = new AzureTableStorageWrapper<QuestionEntity>(options.ConnectionString, options.QuestionTableName);
        }

        public async Task<Guid> Create(Question model)
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
