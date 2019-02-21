using System;
using CandidateEvaluator.Contract.Services;

namespace CandidateEvaluator.Contract.Factories
{
    public interface IMailContentServiceFactory
    {
        IInterviewResultMailContentService GetService(Guid userId);
    }
}
