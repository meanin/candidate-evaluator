using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Common.Responses.InterviewResult
{
    public class InterviewResultResponse
    {
        public Guid Id { get; set; }
        public string CandidateName { get; set; }
        public DateTime InterviewDate { get; set; }
        public List<CategoryResultResponse> Content { get; set; }
    }

    public class CategoryResultResponse
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<QuestionResultResponse> QuestionResults { get; set; }
    }

    public class QuestionResultResponse
    {
        public Guid QuestionId { get; set; }
        public string QuestionName { get; set; }
        public double Score { get; set; }
        public string Notes { get; set; }
    }
}
