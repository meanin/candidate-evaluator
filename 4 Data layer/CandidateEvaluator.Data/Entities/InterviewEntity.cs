using System.Runtime.CompilerServices;
using Microsoft.WindowsAzure.Storage.Table;

[assembly: InternalsVisibleTo("Microsoft.WindowsAzure.Storage.Table")]
namespace CandidateEvaluator.Data.Entities
{
    internal class InterviewEntity : TableEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
