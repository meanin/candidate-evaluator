using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Contract.Commands.Interview
{
    public class CreateInterview : CommandBase
    {
        public string Name { get; set; }
        public Dictionary<Guid, List<Guid>> Content { get; set; } = new Dictionary<Guid, List<Guid>>();
    }
}
