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
                PartitionKey = CreatePartitionKey(model.OwnerId, model.CategoryId),
                RowKey = id.ToString(),
                Name = model.Name,
                Text = model.Text
            };
            await _table.Add(entity);
            return id;
        }

        public Task Delete(Guid ownerId, Guid categoryId, Guid questionId)
        {
            return _table.Delete(CreatePartitionKey(ownerId, categoryId), questionId.ToString());
        }

        public async Task<Question> Get(Guid ownerId, Guid categoryId, Guid questionId)
        {
            var entity = await _table.Get(CreatePartitionKey(ownerId, categoryId), questionId.ToString());
            return ToQuestion(entity);
        }

        public async Task<IEnumerable<Question>> GetAll(Guid ownerId)
        {
            var entities = await _table.GetAll();
            return entities.Select(ToQuestion);
        }

        public async Task<IEnumerable<Question>> GetAllFromPartition(Guid ownerId, Guid categoryId)
        {
            var entities = await _table.GetAll(CreatePartitionKey(ownerId, categoryId));
            return entities.Select(ToQuestion);
        }

        public async Task<Guid> Update(Question model)
        {
            await _table.Update(new QuestionEntity
            {
                PartitionKey = CreatePartitionKey(model.OwnerId, model.CategoryId),
                RowKey = model.Id.ToString(),
                Name = model.Name,
                Text = model.Text
            });
            return model.Id;
        }

        private static string CreatePartitionKey(Guid ownerId, Guid categoryId)
        {
            return $"{ownerId}_{categoryId}";
        }

        private static Question ToQuestion(QuestionEntity entity)
        {
            var partitionKey = entity.PartitionKey.Split("_");
            return new Question
            {
                Id = Guid.Parse(entity.RowKey),
                Name = entity.Name,
                Text = entity.Text,
                OwnerId = Guid.Parse(partitionKey[0]),
                CategoryId = Guid.Parse(partitionKey[1])
            };
        }
    }
}
