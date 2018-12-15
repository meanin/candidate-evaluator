using CandidateEvaluator.Contract.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CandidateEvaluator.Contract.Services
{
    public interface IQuestionService
    {
        Task<Question> Add(Question question);

        Task<List<Question>> GetAll(Guid ownerId);
    }
}
