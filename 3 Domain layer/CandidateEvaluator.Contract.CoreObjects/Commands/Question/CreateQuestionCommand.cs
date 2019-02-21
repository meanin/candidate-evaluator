using System;
using CandidateEvaluator.Contract.CoreObjects.Models;
using CandidateEvaluator.Contract.CQRS.Commands;

namespace CandidateEvaluator.Contract.CoreObjects.Commands.Question
{
    public class CreateQuestionCommand : ICommand
    {
        public Guid OwnerId { get; }
        public string Name { get; }
        public string Text { get; }
        public Guid CategoryId { get; }
        public QuestionType Type { get; }

        public CreateQuestionCommand(
            Guid ownerId, 
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
        }
    }
}
