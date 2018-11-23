using System;
using CandidatesEvaluator.Contract.Models;
using CandidatesEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Data.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        public Guid Create(Question model)
        {
            throw new NotImplementedException();
        }

        public Question Get(Guid categoryId, Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Question model)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
