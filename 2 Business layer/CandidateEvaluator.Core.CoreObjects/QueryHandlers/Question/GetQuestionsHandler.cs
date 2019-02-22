using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.CoreObjects.Queries.Question;
using CandidateEvaluator.Contract.CoreObjects.Repositories;
using CandidateEvaluator.Contract.CQRS.Handlers;

namespace CandidateEvaluator.Core.CoreObjects.QueryHandlers.Question
{
    public class GetQuestionsHandler : IQueryHandler<GetAllQuestionsQuery, IEnumerable<Contract.CoreObjects.Models.Question>>
    {
        private readonly IQuestionRepository _modelRepository;

        public GetQuestionsHandler(IQuestionRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public Task<IEnumerable<Contract.CoreObjects.Models.Question>> Handle(GetAllQuestionsQuery query)
        {
            return query.CategoryId != Guid.Empty 
                ? _modelRepository.GetAllFromCategory(query.OwnerId, query.CategoryId) 
                : _modelRepository.GetAll(query.OwnerId);
        }
    }
}
