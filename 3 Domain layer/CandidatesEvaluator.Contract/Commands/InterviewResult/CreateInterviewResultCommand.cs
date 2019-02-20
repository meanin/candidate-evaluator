using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Contract.Commands.InterviewResult
{
    public class CreateInterviewResultCommand : ICommand
    {
        public Guid OwnerId { get; }
        public string CandidateName { get; }
        public string ReviewerName { get; }
        public string InterviewTemplateName { get; }
        public DateTime InterviewDate { get; }
        public List<CreateCategoryResult> Content { get; }

        public CreateInterviewResultCommand(
            Guid ownerId, 
            string candidateName, 
            string reviewerName, 
            string interviewTemplateName, 
            DateTime interviewDate, 
            List<CreateCategoryResult> content)
        {
            OwnerId = ownerId;
            CandidateName = candidateName;
            ReviewerName = reviewerName;
            InterviewTemplateName = interviewTemplateName;
            InterviewDate = interviewDate;
            Content = content;
        }
    }

    public class CreateCategoryResult
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<CreateQuestionResult> QuestionResults { get; set; }
    }

    public class CreateQuestionResult
    {
        public Guid QuestionId { get; set; }
        public string QuestionName { get; set; }
        public double Score { get; set; }
        public string Notes { get; set; }
    }
}
