using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Queries.Question;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Queries.Question
{
    public class GetQuestionsFromCategoryHandler : IQueryHandler<GetQuestionsFromCategory, List<Contract.Models.Question>>
    {
        private readonly IQuestionRepository _modelRepository;

        public GetQuestionsFromCategoryHandler(IQuestionRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public Task<List<Contract.Models.Question>> Handle(GetQuestionsFromCategory query)
        {
            return _modelRepository.GetAllFromPartition(query.OwnerId, query.CategoryId);
        }
    }
}
