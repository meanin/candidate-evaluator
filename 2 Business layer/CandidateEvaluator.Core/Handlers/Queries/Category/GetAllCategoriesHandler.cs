using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Queries.Category;
using CandidateEvaluator.Contract.Repositories;

namespace CandidateEvaluator.Core.Handlers.Queries.Category
{
    public class GetAllCategoriesHandler : IQueryHandler<GetAllCategories, IEnumerable<Contract.Models.Category>>
    {
        private readonly ICategoryRepository _modelRepository;

        public GetAllCategoriesHandler(ICategoryRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task<IEnumerable<Contract.Models.Category>> Handle(GetAllCategories query)
        {
            return await _modelRepository.GetAll(query.OwnerId);
        }
    }
}
