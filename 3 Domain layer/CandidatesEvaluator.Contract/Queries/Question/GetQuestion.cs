using System;


namespace CandidateEvaluator.Contract.Queries.Question
{
    public class GetQuestion : IQuery<Models.Question>
    {
        public Guid OwnerId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid Id { get; set; }
    }
}
