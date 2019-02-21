using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Common.Requests.Interview
{
    public class CreateInterviewRequest
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
