using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Configuration;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;
using CandidateEvaluator.Data.Entities;
using CandidateEvaluator.Data.Wrappers;

namespace CandidateEvaluator.Data.Repositories
{
    public class UserRecentActivityRepository : IUserRecentActivityRepository
    {
        private readonly AzureTableStorageWrapper<RecentActivityEntity> _table;
        private static int MaxUserItemCount = 10;

        public UserRecentActivityRepository(AzureTableStorageOptions options)
        {
            _table = new AzureTableStorageWrapper<RecentActivityEntity>(options.ConnectionString, options.CategoryTableName);
        }

        public async Task<IEnumerable<RecentActivity>> GetAll(Guid ownerId)
        {
            return (await _table.GetAll(ownerId.ToString())).Select(e => new RecentActivity
            {
                EntityId = Guid.Parse((ReadOnlySpan<char>) e.EntityId),
                Type = e.Type
            });
        }

        public async Task Create(Guid ownerId, RecentActivity userActivity)
        {
            var partitionKey = ownerId.ToString();
            await _table.Add(new RecentActivityEntity
            {
                PartitionKey = partitionKey,
                RowKey = Guid.NewGuid().ToString(),
                EntityId = userActivity.EntityId.ToString(),
                Type = userActivity.Type
            });

            var entities = await _table.GetAll(partitionKey);
            if(entities.Count <= MaxUserItemCount)
                return;
            
            foreach (var recentActivityEntity in entities.OrderBy(e => e.Timestamp).Skip(MaxUserItemCount))
            {
                await _table.Delete(partitionKey, recentActivityEntity.RowKey);
            }
        }
    }
}
