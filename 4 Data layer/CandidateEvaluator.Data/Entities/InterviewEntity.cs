using Microsoft.WindowsAzure.Storage.Table;

namespace CandidateEvaluator.Data.Entities
{
    public class InterviewEntity : TableEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
