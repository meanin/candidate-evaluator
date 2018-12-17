using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Queries.Question;
using CandidateEvaluator.Contract.Repositories;
using System.Threading.Tasks;

namespace CandidateEvaluator.Core.Handlers.Queries
{
    public class GetQuestionHandler : IQueryHandler<GetQuestion, Question>
    {
        private readonly IQuestionRepository _modelRepository;

        public GetQuestionHandler(IQuestionRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public Task<Question> HandleAsync(GetQuestion query)
        {
            return _modelRepository.Get(query.OwnerId, query.CategoryId, query.Id);
        }
    }
}
