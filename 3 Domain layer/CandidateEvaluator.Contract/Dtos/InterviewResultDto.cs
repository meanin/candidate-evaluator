using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Contract.Dtos
{
    public class InterviewResultDto
    {
        public Guid Id { get; set; }
        public string CandidateName { get; set; }
        public string ReviewerName { get; set; }
        public string InterviewTemplateName { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime InterviewDate { get; set; }
        public List<CategoryResult> Content { get; set; }
    }

    public class CategoryResult
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<QuestionResult> QuestionResults { get; set; }
    }

    public class QuestionResult
    {
        public Guid QuestionId { get; set; }
        public string QuestionName { get; set; }
        public double Score { get; set; }
        public string Notes { get; set; }
    }
}
