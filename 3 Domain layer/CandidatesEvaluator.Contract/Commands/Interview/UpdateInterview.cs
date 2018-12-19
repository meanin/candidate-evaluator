using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Contract.Commands.Interview
{
    public class UpdateInterview : CommandBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Tuple<Guid, List<Guid>>> Content { get; set; }
    }
}
