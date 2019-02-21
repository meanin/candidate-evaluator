using System;
using System.Collections.Generic;
using CandidateEvaluator.Contract.CoreObjects.Models;
using CandidateEvaluator.Contract.Models;

namespace CandidateEvaluator.Contract.Dtos
{
    public class InterviewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
        public List<InterviewContentDto> Content { get; set; }
    }

    public class InterviewContentDto
    {
        public Category Category { get; set; }
        public List<Question> Questions { get; set; }
    }
}
