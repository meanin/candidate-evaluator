using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Models;

namespace CandidateEvaluator.Contract.Services
{
    public interface IInterviewResultMailContentService
    {
        Task<(string Type, string Content)> ToMailContent(InterviewResult result);
    }
}
