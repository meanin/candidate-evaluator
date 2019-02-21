﻿using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Contract.Dtos
{
    public class InterviewListDto
    {
        public List<InterviewListElementDto> List { get; set; }
        public class InterviewListElementDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
    }
}
