using CandidateEvaluator.Contract.Models;
using Microsoft.WindowsAzure.Storage.Table;

namespace CandidateEvaluator.Data.Entities
{
    public class RecentActivityEntity : TableEntity
    {
        public EntityType Type { get; set; }
        public string EntityId { get; set; }
    }
}
