using System;
using CandidateEvaluator.Data.Entities;
using CandidateEvaluator.Data.Wrappers;
using CandidatesEvaluator.Contract.Configuration;
using CandidatesEvaluator.Contract.Models;
using CandidatesEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private AzureTableStorageWrapper<CategoryEntity> _azureTableStorageWrapper;

        public CategoryRepository(AzureTableStorageOptions options)
        {
            _azureTableStorageWrapper = new AzureTableStorageWrapper<CategoryEntity>(options.ConnectionString, options.CategoryTableName);
        }

        public Guid Create(Category model)
        {
            throw new NotImplementedException();
        }

        public Category Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Category model)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
