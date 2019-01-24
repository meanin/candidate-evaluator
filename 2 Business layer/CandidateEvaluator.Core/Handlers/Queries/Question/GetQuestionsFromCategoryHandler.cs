using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Queries.Question;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Queries.Question
{
    public class GetQuestionsFromCategoryHandler : IQueryHandler<GetQuestionsFromCategory, IEnumerable<Contract.Models.Question>>
    {
        private readonly IQuestionRepository _modelRepository;

        public GetQuestionsFromCategoryHandler(IQuestionRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public Task<IEnumerable<Contract.Models.Question>> Handle(GetQuestionsFromCategory query)
        {
            return _modelRepository.GetAllFromPartition(query.OwnerId, query.CategoryId);
        }
    }
}
