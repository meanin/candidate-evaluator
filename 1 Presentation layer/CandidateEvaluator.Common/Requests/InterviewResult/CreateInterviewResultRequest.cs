using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Common.Requests.InterviewResult
{
    public class CreateInterviewResultRequest
    {
        public string CandidateName { get; set; }
        public string InterviewTemplateName { get; set; }
        public DateTime InterviewDate { get; set; }
        public List<CreateCategoryResultRequest> Content { get; set; }
    }

    public class CreateCategoryResultRequest
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<CreateQuestionResultRequest> QuestionResults { get; set; }
    }

    public class CreateQuestionResultRequest
    {
        public Guid QuestionId { get; set; }
        public string QuestionName { get; set; }
        public double Score { get; set; }
        public string Notes { get; set; }
    }
}
