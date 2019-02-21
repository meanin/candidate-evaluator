﻿using System;


namespace CandidateEvaluator.Contract.Queries.Question
{
    public class GetQuestionQuery : IQuery<Models.Question>
    {
        public Guid OwnerId { get; }
        public Guid Id { get; }

        public GetQuestionQuery(Guid ownerId, Guid id)
        {
            OwnerId = ownerId;
            Id = id;
        }
    }
}
