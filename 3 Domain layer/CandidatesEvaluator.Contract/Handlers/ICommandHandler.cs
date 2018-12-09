using CandidateEvaluator.Contract.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateEvaluator.Contract.Handlers
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task<Guid> HandleAsync(T command);
    }
}
