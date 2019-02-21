using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Common.Requests.Interview
{
    public class UpdateInterviewRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<UpdateInterviewCategory> Content { get; set; } = new List<UpdateInterviewCategory>();

        public class UpdateInterviewCategory
        {
            public Guid CategoryId { get; set; }
            public int QuestionCount { get; set; }
        }
    }
}
