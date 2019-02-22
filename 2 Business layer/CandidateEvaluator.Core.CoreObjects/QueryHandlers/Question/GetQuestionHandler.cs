using System.Threading.Tasks;
using CandidateEvaluator.Contract.CoreObjects.Queries.Question;
using CandidateEvaluator.Contract.CoreObjects.Repositories;
using CandidateEvaluator.Contract.CQRS.Handlers;

namespace CandidateEvaluator.Core.CoreObjects.QueryHandlers.Question
{
    public class GetQuestionHandler : IQueryHandler<GetQuestionQuery, Contract.CoreObjects.Models.Question>
    {
        private readonly IQuestionRepository _modelRepository;

        public GetQuestionHandler(IQuestionRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public Task<Contract.CoreObjects.Models.Question> Handle(GetQuestionQuery query)
        {
            return _modelRepository.Get(query.OwnerId, query.Id);
        }
    }
}
