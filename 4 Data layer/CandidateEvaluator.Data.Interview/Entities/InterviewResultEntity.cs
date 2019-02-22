using System;
using System.Runtime.CompilerServices;
using Microsoft.WindowsAzure.Storage.Table;

[assembly: InternalsVisibleTo("Microsoft.WindowsAzure.Storage.Table")]
namespace CandidateEvaluator.Data.Interview.Entities
{
    internal class InterviewResultEntity : TableEntity
    {
        public string CandidateName { get; set; }
        public DateTime InterviewDate { get; set; }
        public string Content { get; set; }
    }
}
