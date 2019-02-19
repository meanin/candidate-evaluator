using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Models;

namespace CandidateEvaluator.Contract.Services
{
    public interface IMailService
    {
        Task SendInterviewResultReport(InterviewResult result, Guid userId);
    }
}
