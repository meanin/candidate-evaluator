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

        public async Task<Question> Create(Question model)
        {
            var id = Guid.NewGuid();
            var entity = new QuestionEntity
            {
                PartitionKey = model.CategoryId.ToString(),
                RowKey = id.ToString(),
                Name = model.Name,
                Text = model.Text
            };

            await _table.Add(entity);

            model.Id = id;

            return model;
        }

        public async Task<Question> Get(Guid categoryId, Guid id)
        {
            var entity = await _table.Get(categoryId.ToString(), id.ToString());

            return new Question
            {
                CategoryId = Guid.Parse(entity.PartitionKey),
                Id = Guid.Parse(entity.RowKey),
                Name = entity.Name,
                Text = entity.Text
            };
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
