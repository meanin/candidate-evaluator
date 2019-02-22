using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Configuration;
using CandidateEvaluator.Contract.Interview.Repositories;
using CandidateEvaluator.Data.Interview.Entities;
using CandidateEvaluator.Data.Wrappers;
using Newtonsoft.Json;

namespace CandidateEvaluator.Data.Interview.Repositories
{
    public class InterviewRepository : IInterviewRepository
    {
        private readonly AzureTableStorageWrapper<InterviewEntity> _table;

        public InterviewRepository(AzureTableStorageOptions options)
        {
            _table = new AzureTableStorageWrapper<InterviewEntity>(options.ConnectionString, options.InterviewTableName);
        }

        public async Task<Guid> Add(Contract.Interview.Models.Interview model)
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

        public async Task<IEnumerable<Contract.Interview.Models.Interview>> GetAll(Guid ownerId)
        {
            var entities = await _table.GetAll(ownerId.ToString());
            return entities.Select(e => new Contract.Interview.Models.Interview
            {
                Id = Guid.Parse(e.RowKey),
                Name = e.Name,
                OwnerId = ownerId,
                Content = JsonConvert.DeserializeObject<Dictionary<Guid, int>>(e.Content)
            });
        }

        public async Task<Contract.Interview.Models.Interview> Get(Guid ownerId, Guid id)
        {
            var entity = await _table.Get(ownerId.ToString(), id.ToString());
            return new Contract.Interview.Models.Interview
            {
                Id = Guid.Parse((ReadOnlySpan<char>)entity.RowKey),
                Name = entity.Name,
                OwnerId = ownerId,
                Content = JsonConvert.DeserializeObject<Dictionary<Guid, int>>(entity.Content)
            };
        }

        public async Task<Guid> Update(Contract.Interview.Models.Interview model)
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
