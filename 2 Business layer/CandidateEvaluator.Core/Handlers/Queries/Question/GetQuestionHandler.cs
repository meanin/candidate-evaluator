using System.Threading.Tasks;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Queries.Question;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Queries.Question
{
    public class GetQuestionHandler : IQueryHandler<GetQuestion, Contract.Models.Question>
    {
        private readonly IQuestionRepository _modelRepository;

        public GetQuestionHandler(IQuestionRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public Task<Contract.Models.Question> Handle(GetQuestion query)
        {
            return _modelRepository.Get(query.OwnerId, query.CategoryId, query.Id);
        }
    }
}
