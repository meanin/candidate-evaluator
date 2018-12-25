using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Contract.Models
{
    public class Interview
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
        public Dictionary<Guid, int> Content { get; set; }
    }
}
