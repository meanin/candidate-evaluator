using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Contract.Commands.Interview
{
    public class CreateInterview : CommandBase
    {
        public string Name { get; set; }
        public List<CreateInterviewCategory> Content { get; set; } = new List<CreateInterviewCategory>();
    }

    public class CreateInterviewCategory
    {
        public Guid CategoryId { get; set; }
        public int QuestionCount { get; set; }
    }
}
