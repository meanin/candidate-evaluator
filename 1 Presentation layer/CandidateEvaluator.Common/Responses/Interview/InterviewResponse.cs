using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Common.Responses.Interview
{
    public class InterviewResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
        public List<InterviewContentResponse> Content { get; set; }
    }

    public class InterviewContentResponse
    {
        public Guid CategoryId { get; set; }
        public int QuestionCount { get; set; }
    }
}
