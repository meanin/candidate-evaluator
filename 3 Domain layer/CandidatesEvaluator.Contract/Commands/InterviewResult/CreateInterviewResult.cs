﻿using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Contract.Commands.InterviewResult
{
    public class CreateInterviewResult : CommandBase
    {
        public string CandidateName { get; set; }
        public DateTime InterviewDate { get; set; }
        public List<CreateCategoryResult> Content { get; set; }
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