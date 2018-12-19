namespace CandidateEvaluator.Contract.Configuration
{
    public class AzureTableStorageOptions
    {
        public string ConnectionString { get; set; }
        public string CategoryTableName { get; set; }
        public string QuestionTableName { get; set; }
        public string InterviewTableName { get; set; }
        public string UserRecentActivityTableName { get; set; }
    }
}
