using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Common.Dtos
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
        public CategoryDto Category { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
}
