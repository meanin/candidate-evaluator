using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Common.Responses.Interview
{
    public class StartInterviewResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<StartInterviewContentResponse> Content { get; set; }
    }

    public class StartInterviewContentResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<StartInterviewQuestionResponse> Questions { get; set; }
    }

    public class StartInterviewQuestionResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
    }
}
