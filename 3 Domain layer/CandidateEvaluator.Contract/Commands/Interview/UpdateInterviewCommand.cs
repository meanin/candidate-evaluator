using System;
using System.Collections.Generic;
using CandidateEvaluator.Contract.CQRS.Commands;

namespace CandidateEvaluator.Contract.Commands.Interview
{
    public class UpdateInterviewCommand : ICommand
    {
        public Guid OwnerId { get; }
        public Guid Id { get; }
        public string Name { get; }
        public List<(Guid CategoryId, int QuestionCount)> Content { get; }

        public UpdateInterviewCommand(Guid ownerId, Guid id, string name, List<(Guid CategoryId, int QuestionCount)> content)
        {
            OwnerId = ownerId;
            Name = name;
            Content = content;
            Id = id;
        }
    }
}
