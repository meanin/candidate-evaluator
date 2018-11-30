using Microsoft.WindowsAzure.Storage.Table;

namespace CandidateEvaluator.Data.Entities
{
    public class CategoryEntity : TableEntity
    {
        public string Name { get; set; }
    }
}
