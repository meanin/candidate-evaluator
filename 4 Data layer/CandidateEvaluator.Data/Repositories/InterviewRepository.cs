using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Configuration;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;
using CandidateEvaluator.Data.Entities;
using CandidateEvaluator.Data.Wrappers;
using Newtonsoft.Json;

namespace CandidateEvaluator.Data.Repositories
{
    public class InterviewRepository : IInterviewRepository
    {
        private readonly AzureTableStorageWrapper<InterviewEntity> _table;

        public InterviewRepository(AzureTableStorageOptions options)
        {
            _table = new AzureTableStorageWrapper<InterviewEntity>(options.ConnectionString, options.InterviewTableName);
        }
        public async Task<Guid> Add(Interview model)
        {
            var id = Guid.NewGuid();
            var entity = new InterviewEntity
            {
                PartitionKey = model.OwnerId.ToString(),
                RowKey = id.ToString(),
                Name = model.Name,
                Content = JsonConvert.SerializeObject(model.Content)
            };
            await _table.Add(entity);
            return id;
        }

        public async Task<List<Interview>> GetAll(Guid ownerId)
        {
            var entities = await _table.GetAll(ownerId.ToString());
            return entities.Select(e => new Interview
            {
                Id = Guid.Parse(e.RowKey),
                Name = e.Name,
                OwnerId = ownerId,
                Content = JsonConvert.DeserializeObject<Dictionary<Guid, List<Guid>>>(e.Content)
            }).ToList();
        }

        public async Task<Interview> Get(Guid ownerId, Guid id)
        {
            var entity = await _table.Get(ownerId.ToString(), id.ToString());
            return new Interview
            {
                Id = Guid.Parse((ReadOnlySpan<char>)entity.RowKey),
                Name = entity.Name,
                OwnerId = ownerId,
                Content = JsonConvert.DeserializeObject<Dictionary<Guid, List<Guid>>>(entity.Content)
            };
        }

        public async Task<Guid> Update(Interview model)
        {
            await _table.Update(new InterviewEntity
            {
                PartitionKey = model.OwnerId.ToString(),
                RowKey = model.Id.ToString(),
                Name = model.Name,
                Content = JsonConvert.SerializeObject(model.Content)
            });
            return model.Id;
        }

        public Task Delete(Guid ownerId, Guid id)
        {
            return _table.Delete(ownerId.ToString(), id.ToString());
        }
    }
}
