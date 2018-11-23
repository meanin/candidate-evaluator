using System;

namespace CandidatesEvaluator.Contract.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message)
        : base(message)
        {
        }
    }
}
