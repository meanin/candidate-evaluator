using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Data.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        public Task<Guid> Create(Question model)
        {
            throw new NotImplementedException();
        }

        public Task<Question> Get(Guid categoryId, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Question model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
