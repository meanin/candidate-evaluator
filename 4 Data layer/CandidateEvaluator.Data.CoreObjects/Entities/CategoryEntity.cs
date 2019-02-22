using System.Runtime.CompilerServices;
using Microsoft.WindowsAzure.Storage.Table;

[assembly: InternalsVisibleTo("Microsoft.WindowsAzure.Storage.Table")]
namespace CandidateEvaluator.Data.CoreObjects.Entities
{
    internal class CategoryEntity : TableEntity
    {
        public string Name { get; set; }
    }
}
