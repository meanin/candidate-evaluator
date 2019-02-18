using System.Threading.Tasks;
using CandidateEvaluator.Contract.Models;

namespace CandidateEvaluator.Contract.Services
{
    public interface IMailClient
    {
        Task SendInterviewResultReport(InterviewResult result);
    }
}
