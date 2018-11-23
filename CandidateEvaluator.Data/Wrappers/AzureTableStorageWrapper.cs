using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Exceptions;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace CandidateEvaluator.Data.Wrappers
{
    internal class AzureTableStorageWrapper<TEntity> where TEntity : TableEntity, new()
    {
        private readonly CloudTable _table;

        public AzureTableStorageWrapper(string connectionString, string tableName)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var tableClient = storageAccount.CreateCloudTableClient();
            _table = tableClient.GetTableReference(tableName);
        }

        public async Task<TEntity> Get(string partitionKey, string rowKey)
        {
            var tableOperation = TableOperation.Retrieve<TEntity>(partitionKey, rowKey);
            var tableResult = await _table.ExecuteAsync(tableOperation);

            if (tableResult.Result is TEntity entity)
            {
                return entity;
            }

            throw new NotFoundException(
                $"No {typeof(TEntity).Name} with partitionKey: {partitionKey} and rowKey: {rowKey} found");
        }

        public async Task<List<TEntity>> GetAll(string partitionKey = "")
        {
            try
            {
                var tableQuery = partitionKey == string.Empty
                    ? new TableQuery<TEntity>()
                    : new TableQuery<TEntity>()
                        .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));
                var results = new List<TEntity>();
                TableContinuationToken continuationToken = null;
                do
                {
                    var segment = await _table.ExecuteQuerySegmentedAsync(tableQuery, continuationToken);
                    continuationToken = segment.ContinuationToken;
                    results.AddRange(segment.Results);
                }
                while (continuationToken != null);

                return results;
            }
            catch (StorageException e) when (e.Message == "Not Found")
            {
                return new List<TEntity>();
            }

        }

        public Task Add(TEntity entity)
        {
            var tableOperation = TableOperation.Insert(entity);
            return _table.ExecuteAsync(tableOperation);
        }

        public async Task Update(TEntity entity)
        {
            entity.ETag = "*";
            try
            {
                var tableOperation = TableOperation.Replace(entity);
                await _table.ExecuteAsync(tableOperation);
            }
            catch (StorageException e) when (e.Message == "Not Found")
            {
                throw new NotFoundException($"No {typeof(TEntity).Name} with partitionKey: {entity.PartitionKey} and rowKey: {entity.RowKey} found");
            }
        }

        public async Task Delete(string partitionKey, string rowKey)
        {
            var entity = await Get(partitionKey, rowKey);

            if (entity == null)
            {
                return;
            }

            var tableOperation = TableOperation.Delete(entity);
            await _table.ExecuteAsync(tableOperation);
        }

    }
}
