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
        private const string PartitionKey = "Category";

        public CategoryRepository(AzureTableStorageOptions options)
        {
            _table = new AzureTableStorageWrapper<CategoryEntity>(options.ConnectionString, options.CategoryTableName);
        }

        public async Task<Category> Add(Category model)
        {
            var id = Guid.NewGuid();
            var entity = new CategoryEntity
            {
                PartitionKey = PartitionKey,
                RowKey = id.ToString(),
                Name = model.Name
            };
            await _table.Add(entity);
            model.Id = id;
            return model;
        }

        public async Task<List<Category>> GetAll()
        {
            var entities = await _table.GetAll(PartitionKey);
            return entities.Select(e => new Category
            {
                Id = Guid.Parse(e.RowKey),
                Name = e.Name
            }).ToList();
        }

        public async Task<Category> Get(Guid id)
        {
            var entity = await _table.Get(PartitionKey, id.ToString());
            return new Category
            {
                Id = Guid.Parse((ReadOnlySpan<char>) entity.RowKey),
                Name = entity.Name
            };
        }

        public Task Update(Category model)
        {
            return _table.Update(new CategoryEntity
            {
                PartitionKey = PartitionKey,
                RowKey = model.Id.ToString(),
                Name = model.Name
            });
        }

        public Task Delete(Guid id)
        {
            return _table.Delete(PartitionKey, id.ToString());
        }
    }
}
