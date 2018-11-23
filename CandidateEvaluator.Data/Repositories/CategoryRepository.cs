using System;
using System.Threading.Tasks;
using CandidateEvaluator.Data.Entities;
using CandidateEvaluator.Data.Wrappers;
using CandidatesEvaluator.Contract.Configuration;
using CandidatesEvaluator.Contract.Models;
using CandidatesEvaluator.Contract.Repositories;

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

        public Task<Guid> Add(Category model)
        {
            var id = Guid.NewGuid();
            var entity = new CategoryEntity
            {
                PartitionKey = PartitionKey,
                RowKey = id.ToString(),
                Name = model.Name
            };
            _table.Add(entity);
            return Task.FromResult(id);
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
