using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Queries;
using CandidateEvaluator.Contract.Repositories;
using CandidateEvaluator.Contract.Services;
using System.Threading.Tasks;

namespace CandidateEvaluator.Core.Handlers.Queries
{
    public class GetCategoryHandler : IQueryHandler<GetCategory, Category>
    {
        private readonly ICategoryRepository _modelRepository;

        public GetCategoryHandler(ICategoryRepository modelRepository)
        {
            this._modelRepository = modelRepository;
        }

        public async Task<Category> HandleAsync(GetCategory query)
        {
            return await _modelRepository.Get(query.OwnerId, query.Id);
        }
    }
}
