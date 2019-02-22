using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Configuration;
using CandidateEvaluator.Contract.Interview.Models;
using CandidateEvaluator.Contract.Interview.Repositories;
using CandidateEvaluator.Data.Interview.Entities;
using CandidateEvaluator.Data.Wrappers;
using Newtonsoft.Json;

namespace CandidateEvaluator.Data.Interview.Repositories
{
    public class InterviewResultRepository : IInterviewResultRepository
    {
        private readonly AzureTableStorageWrapper<InterviewResultEntity> _table;

        public InterviewResultRepository(AzureTableStorageOptions options)
        {
            _table = new AzureTableStorageWrapper<InterviewResultEntity>(options.ConnectionString,
                options.InterviewResultTableName);
        }

        public async Task<Guid> Add(InterviewResult model)
        {
            var id = Guid.NewGuid();
            var entity = new InterviewResultEntity
            {
                PartitionKey = model.OwnerId.ToString(),
                RowKey = id.ToString(),
                CandidateName = model.CandidateName,
                InterviewDate = model.InterviewDate,
                Content = JsonConvert.SerializeObject(model.Content)
            };
            await _table.Add(entity);
            return id;
        }

        public async Task<IEnumerable<InterviewResult>> GetAll(Guid ownerId)
        {
            var entities = await _table.GetAll(ownerId.ToString());
            return entities.Select(e => new InterviewResult
            {
                Id = Guid.Parse((ReadOnlySpan<char>)e.RowKey),
                OwnerId = ownerId,
                CandidateName = e.CandidateName,
                InterviewDate = e.InterviewDate,
                Content = JsonConvert.DeserializeObject<List<CategoryResult>>(e.Content)
            });
        }

        public async Task<InterviewResult> Get(Guid ownerId, Guid id)
        {
            var entity = await _table.Get(ownerId.ToString(), id.ToString());
            return new InterviewResult
            {
                Id = Guid.Parse((ReadOnlySpan<char>)entity.RowKey),
                OwnerId = ownerId,
                CandidateName = entity.CandidateName,
                InterviewDate = entity.InterviewDate,
                Content = JsonConvert.DeserializeObject<List<CategoryResult>>(entity.Content)
            };
        }

        public Task Delete(Guid ownerId, Guid id)
        {
            return _table.Delete(ownerId.ToString(), id.ToString());
        }
    }
}
