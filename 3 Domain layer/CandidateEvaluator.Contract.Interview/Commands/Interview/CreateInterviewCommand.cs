using System;
using System.Collections.Generic;
using CandidateEvaluator.Contract.CQRS.Commands;

namespace CandidateEvaluator.Contract.Interview.Commands.Interview
{
    public class CreateInterviewCommand : ICommand
    {
        public Guid OwnerId { get; }
        public string Name { get; }
        public List<(Guid CategoryId, int QuestionCount)> Content { get; }

        public CreateInterviewCommand(Guid ownerId, string name, List<(Guid CategoryId, int QuestionCount)> content)
        {
            OwnerId = ownerId;
            Name = name;
            Content = content;
        }
    }
}
