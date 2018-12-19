using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Queries.Question;
using CandidateEvaluator.Contract.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CandidateEvaluator.Core.Handlers.Queries
{
    public class GetQuestionsFromCategoryHandler : IQueryHandler<GetQuestionsFromCategory, List<Question>>
    {
        private readonly IQuestionRepository _modelRepository;

        public GetQuestionsFromCategoryHandler(IQuestionRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public Task<List<Question>> HandleAsync(GetQuestionsFromCategory query)
        {
            return _modelRepository.GetAllFromPartition(query.OwnerId, query.CategoryId);
        }
    }
}
