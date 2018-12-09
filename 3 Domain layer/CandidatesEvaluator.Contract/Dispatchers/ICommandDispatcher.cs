using CandidateEvaluator.Contract.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateEvaluator.Contract.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task<Guid> SendAsync<T>(T command) where T : ICommand;
    }
}
