using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Data.Entities;
using CandidateEvaluator.Data.Wrappers;
using CandidateEvaluator.Contract.Configuration;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AzureTableStorageWrapper<CategoryEntity> _table;

        public CategoryRepository(AzureTableStorageOptions options)
        {
            _table = new AzureTableStorageWrapper<CategoryEntity>(options.ConnectionString, options.CategoryTableName);
        }

        public async Task<Category> Add(Category model)
        {
            var id = Guid.NewGuid();
            var entity = new CategoryEntity
            {
                PartitionKey = model.OwnerId.ToString(),
                RowKey = id.ToString(),
                Name = model.Name
            };
            await _table.Add(entity);
            model.Id = id;
            return model;
        }

        public async Task<List<Category>> GetAll(Guid ownerId)
        {
            var entities = await _table.GetAll(ownerId.ToString());
            return entities.Select(e => new Category
            {
                Id = Guid.Parse(e.RowKey),
                Name = e.Name,
                OwnerId = ownerId
            }).ToList();
        }

        public async Task<Category> Get(Guid ownerId, Guid id)
        {
            var entity = await _table.Get(ownerId.ToString(), id.ToString());
            return new Category
            {
                Id = Guid.Parse((ReadOnlySpan<char>) entity.RowKey),
                Name = entity.Name,
                OwnerId = ownerId
            };
        }

        public Task Update(Category model)
        {
            return _table.Update(new CategoryEntity
            {
                PartitionKey = model.OwnerId.ToString(),
                RowKey = model.Id.ToString(),
                Name = model.Name
            });
        }

        public Task Delete(Guid ownerId, Guid id)
        {
            return _table.Delete(ownerId.ToString(), id.ToString());
        }
    }
}
