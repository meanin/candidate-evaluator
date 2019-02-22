using System;
using System.Collections.Generic;
using CandidateEvaluator.Contract.CoreObjects.Models;

namespace CandidateEvaluator.Contract.Interview.Models
{
    public class StartInterview
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
        public List<StartInterviewContent> Content { get; set; }
    }

    public class StartInterviewContent
    {
        public Category Category { get; set; }
        public List<Question> Questions { get; set; }
    }
}
