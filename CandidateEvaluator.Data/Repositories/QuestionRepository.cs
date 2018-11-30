using CandidateEvaluator.Contract.Configuration;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;
using CandidateEvaluator.Data.Entities;
using CandidateEvaluator.Data.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task Delete(Guid categoryId, Guid questionId)
        {
            return _table.Delete(categoryId.ToString(), questionId.ToString());
        }

        public async Task<Question> Get(Guid categoryId, Guid questionId)
        {
            var entity = await _table.Get(categoryId.ToString(), questionId.ToString());

            return new Question
            {
                CategoryId = Guid.Parse(entity.PartitionKey),
                Id = Guid.Parse(entity.RowKey),
                Name = entity.Name,
                Text = entity.Text
            };
        }

        public async Task<List<Question>> GetAll()
        {
            var entities = await _table.GetAll();

            return entities.Select(e => new Question
            {
                CategoryId = Guid.Parse(e.PartitionKey),
                Id = Guid.Parse(e.RowKey),
                Name = e.Name,
                Text = e.Text
            }
            ).ToList();
        }

        public async Task<List<Question>> GetAllFromPartition(Guid partitionKey)
        {
            var entities = await _table.GetAll(partitionKey.ToString());

            return entities.Select(e => new Question
            {
                CategoryId = Guid.Parse(e.PartitionKey),
                Id = Guid.Parse(e.RowKey),
                Name = e.Name,
                Text = e.Text
            }
            ).ToList();
        }

        public Task Update(Question model)
        {
            return _table.Update(new QuestionEntity
            {
                PartitionKey = model.CategoryId.ToString(),
                RowKey = model.Id.ToString(),
                Name = model.Name,
                Text = model.Text
            });
        }
    }
}
