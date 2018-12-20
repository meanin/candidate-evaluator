using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Contract.Dtos
{
    public class InterviewListDto
    {
        public List<InterviewListElement> List { get; set; }
        public class InterviewListElement
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
    }

}
