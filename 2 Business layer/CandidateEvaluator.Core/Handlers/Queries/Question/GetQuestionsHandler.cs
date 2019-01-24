using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Queries.Question;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Queries.Question
{
    public class GetQuestionsHandler : IQueryHandler<GetQuestions, IEnumerable<Contract.Models.Question>>
    {
        private readonly IQuestionRepository _modelRepository;

        public GetQuestionsHandler(IQuestionRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public Task<IEnumerable<Contract.Models.Question>> Handle(GetQuestions query)
        {
            return query.CategoryId != Guid.Empty 
                ? _modelRepository.GetAllFromCategory(query.OwnerId, query.CategoryId) 
                : _modelRepository.GetAll(query.OwnerId);
        }
    }
}
