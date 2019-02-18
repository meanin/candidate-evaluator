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
    public class PublishedCategoryRepository : IPublishedCategoryRepository
    {
        private readonly AzureTableStorageWrapper<PublishedCategoryEntity> _table;

        public PublishedCategoryRepository(AzureTableStorageOptions options)
        {
            _table = new AzureTableStorageWrapper<PublishedCategoryEntity>(options.ConnectionString, options.CategoryTableName);
        }

        public async Task<Guid> Add(PublishedCategory model)
        {
            var id = Guid.NewGuid();
            var entity = new PublishedCategoryEntity
            {
                PartitionKey = string.Empty,
                RowKey = id.ToString(),
                Name = model.Name
            };
            await _table.Add(entity);
            return id;
        }

        public async Task<IEnumerable<PublishedCategory>> GetAll()
        {
            var entities = await _table.GetAll(string.Empty);
            return entities.Select(e => new PublishedCategory
            {
                Id = Guid.Parse(e.RowKey),
                Name = e.Name
            });
        }

        public async Task<PublishedCategory> Get(Guid id)
        {
            var entity = await _table.Get(string.Empty, id.ToString());
            return new PublishedCategory
            {
                Id = Guid.Parse((ReadOnlySpan<char>) entity.RowKey),
                Name = entity.Name
            };
        }

        public async Task<Guid> Update(PublishedCategory model)
        {
            await _table.Update(new PublishedCategoryEntity
            {
                PartitionKey = string.Empty,
                RowKey = model.Id.ToString(),
                Name = model.Name
            });
            return model.Id;
        }

        public Task Delete(Guid id)
        {
            return _table.Delete(string.Empty, id.ToString());
        }
    }
}
