using Microsoft.WindowsAzure.Storage.Table;

namespace CandidateEvaluator.Data.Entities
{
    public class QuestionEntity : TableEntity
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
