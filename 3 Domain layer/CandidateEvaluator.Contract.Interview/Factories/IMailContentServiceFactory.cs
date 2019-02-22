using System;
using CandidateEvaluator.Contract.Interview.Services;

namespace CandidateEvaluator.Contract.Interview.Factories
{
    public interface IMailContentServiceFactory
    {
        IInterviewResultMailContentService GetService(Guid userId);
    }
}
