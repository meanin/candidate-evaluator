using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace CandidateEvaluator.Data.Entities
{
    public class InterviewResultEntity : TableEntity
    {
        public string CandidateName { get; set; }
        public DateTime InterviewDate { get; set; }
        public string Content { get; set; }
    }
}
