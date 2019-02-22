using System;
using System.Collections.Generic;
using CandidateEvaluator.Contract.CQRS.Queries;

namespace CandidateEvaluator.Contract.CoreObjects.Queries.Question
{
    public class GetAllQuestionsQuery : IQuery<IEnumerable<Models.Question>>
    {
        public Guid OwnerId { get; }
        public Guid CategoryId { get; }

        public GetAllQuestionsQuery(Guid ownerId, Guid categoryId)
        {
            OwnerId = ownerId;
            CategoryId = categoryId;
        }
    }
}
