using System;

namespace CandidateEvaluator.Common.Responses.InterviewResult
{
    public class MiniInterviewResultResponse
    {
        public Guid Id { get; set; }
        public string CandidateName { get; set; }
        public DateTime InterviewDate { get; set; }
    }
}
