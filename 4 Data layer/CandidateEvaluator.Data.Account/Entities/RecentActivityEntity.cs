using System.Runtime.CompilerServices;
using Microsoft.WindowsAzure.Storage.Table;

[assembly: InternalsVisibleTo("Microsoft.WindowsAzure.Storage.Table")]
namespace CandidateEvaluator.Data.Account.Entities
{
    internal class RecentActivityEntity : TableEntity
    {
        public string Type { get; set; }
        public string EntityId { get; set; }
        public string Name { get; set; }
    }
}
