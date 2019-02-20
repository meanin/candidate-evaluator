﻿using System;
using CandidateEvaluator.Contract.Models;

namespace CandidateEvaluator.Contract.Commands.Question
{
    public class UpdateQuestionCommand : ICommand
    {
        public Guid OwnerId { get; }
        public Guid Id { get; }
        public string Name { get; }
        public string Text { get; }
        public Guid CategoryId { get; }
        public QuestionType Type { get; }

        public UpdateQuestionCommand(
            Guid ownerId, 
            Guid id,
            string name,
            string text,
            Guid categoryId,
            QuestionType type)
        {
            OwnerId = ownerId;
            Name = name;
            Text = text;
            CategoryId = categoryId;
            Type = type;
            Id = id;
        }
    }
}
