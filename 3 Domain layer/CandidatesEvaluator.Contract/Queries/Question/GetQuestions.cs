using System;
using System.Collections.Generic;

namespace CandidateEvaluator.Contract.Queries.Question
{
    public class GetQuestions : IQuery<IEnumerable<Models.Question>>
    {
        public Guid CategoryId { get; set; }

        public Guid OwnerId { get; set; }
    }
}
