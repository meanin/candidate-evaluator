using System.Threading.Tasks;
using CandidateEvaluator.Contract.Interview.Models;

namespace CandidateEvaluator.Contract.Interview.Services
{
    public interface IInterviewResultMailContentService
    {
        Task<(string Type, string Content)> ToMailContent(InterviewResult result);
    }
}
