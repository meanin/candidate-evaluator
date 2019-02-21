using System.Threading.Tasks;
using CandidateEvaluator.Contract.CoreObjects.Queries.Category;
using CandidateEvaluator.Contract.CoreObjects.Repositories;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Queries.Category
{
    public class GetCategoryHandler : IQueryHandler<GetCategoryQuery, Contract.CoreObjects.Models.Category>
    {
        private readonly ICategoryRepository _modelRepository;

        public GetCategoryHandler(ICategoryRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task<Contract.CoreObjects.Models.Category> Handle(GetCategoryQuery query)
        {
            return await _modelRepository.Get(query.OwnerId, query.Id);
        }
    }
}
