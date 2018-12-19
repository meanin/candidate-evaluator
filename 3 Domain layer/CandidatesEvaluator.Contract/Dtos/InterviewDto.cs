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
        public Dictionary<Category, List<Question>> Content { get; set; }
    }
}
