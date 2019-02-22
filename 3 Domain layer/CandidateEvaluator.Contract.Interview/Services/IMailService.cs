using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Interview.Models;

namespace CandidateEvaluator.Contract.Interview.Services
{
    public interface IMailService
    {
        Task SendInterviewResultReport(InterviewResult result, Guid userId);
    }
}
