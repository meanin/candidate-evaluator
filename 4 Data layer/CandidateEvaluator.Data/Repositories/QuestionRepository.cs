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
        private readonly AzureTableStorageWrapper<QuestionEntity> _table;

        public QuestionRepository(AzureTableStorageOptions options)
        {
            _table = new AzureTableStorageWrapper<QuestionEntity>(options.ConnectionString, options.QuestionTableName);
        }

        public async Task<Guid> Create(Question model)
        {
            var id = Guid.NewGuid();
            var entity = new QuestionEntity
            {
                PartitionKey = model.OwnerId.ToString(),
                RowKey = id.ToString(),
                Name = model.Name,
                Text = model.Text,
                CategoryId = model.CategoryId.ToString()
            };
            await _table.Add(entity);
            return id;
        }

        public Task Delete(Guid ownerId, Guid categoryId, Guid questionId)
        {
            return _table.Delete(ownerId.ToString(), questionId.ToString());
        }

        public async Task<Question> Get(Guid ownerId, Guid categoryId, Guid questionId)
        {
            var entity = await _table.Get(ownerId.ToString(), questionId.ToString());
            return ToQuestion(entity);
        }

        public async Task<IEnumerable<Question>> GetAll(Guid ownerId)
        {
            var entities = await _table.GetAll();
            return entities.Select(ToQuestion);
        }

        public async Task<IEnumerable<Question>> GetAllFromCategory(Guid ownerId, Guid categoryId)
        {
            var entities = await _table.GetAll(ownerId.ToString());
            return entities.Where(e => e.CategoryId == categoryId.ToString()).Select(ToQuestion);
        }

        public async Task<Guid> Update(Question model)
        {
            await _table.Update(new QuestionEntity
            {
                PartitionKey = model.OwnerId.ToString(),
                RowKey = model.Id.ToString(),
                Name = model.Name,
                Text = model.Text,
                CategoryId = model.CategoryId.ToString()
            });
            return model.Id;
        }

        private static Question ToQuestion(QuestionEntity entity)
        {
            return new Question
            {
                Id = Guid.Parse(entity.RowKey),
                Name = entity.Name,
                Text = entity.Text,
                OwnerId = Guid.Parse(entity.PartitionKey),
                CategoryId = Guid.Parse(entity.CategoryId)
            };
        }
    }
}
