using System;

namespace CandidateEvaluator.Contract.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public Guid Oid { get; set; }
    }
}
