using System;

namespace CandidateEvaluator.Contract.Commands
{
    public class CommandBase : ICommand
    {
        public Guid OwnerId { get; set; }
    }
}
