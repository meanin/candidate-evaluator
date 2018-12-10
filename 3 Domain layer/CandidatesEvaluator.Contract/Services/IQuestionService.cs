using CandidateEvaluator.Contract.Models;
using System.Threading.Tasks;

namespace CandidateEvaluator.Contract.Services
{
    public interface IQuestionService
    {
        Task<Question> Add(Question question);
    }
}
