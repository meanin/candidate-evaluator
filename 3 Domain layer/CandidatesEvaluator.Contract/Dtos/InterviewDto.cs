using System;
using System.Collections.Generic;
using CandidateEvaluator.Contract.Models;

namespace CandidateEvaluator.Contract.Dtos
{
    public class InterviewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
        public List<InterviewContent> Content { get; set; }
    }

    public class InterviewContent
    {
        public Category Category { get; set; }
        public List<Question> Questions { get; set; }
    }
}
