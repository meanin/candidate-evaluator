using System;

namespace CandidateEvaluator.Contract.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message)
        : base(message)
        {
        }
    }
}
