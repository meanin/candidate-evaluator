using Microsoft.WindowsAzure.Storage.Table;

namespace CandidateEvaluator.Data.Entities
{
    public class RecentActivityEntity : TableEntity
    {
        public string Type { get; set; }
        public string EntityId { get; set; }
        public string Name { get; set; }
    }
}
