using System.Threading.Tasks;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Queries.Category;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Queries.Category
{
    public class GetCategoryHandler : IQueryHandler<GetCategory, Contract.Models.Category>
    {
        private readonly ICategoryRepository _modelRepository;

        public GetCategoryHandler(ICategoryRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task<Contract.Models.Category> Handle(GetCategory query)
        {
            return await _modelRepository.Get(query.OwnerId, query.Id);
        }
    }
}
