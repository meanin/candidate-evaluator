using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.CoreObjects.Queries.Category;
using CandidateEvaluator.Contract.CoreObjects.Repositories;
using CandidateEvaluator.Contract.CQRS.Handlers;

namespace CandidateEvaluator.Core.CoreObjects.QueryHandlers.Category
{
    public class GetAllCategoriesHandler : IQueryHandler<GetAllCategoriesQuery, IEnumerable<Contract.CoreObjects.Models.Category>>
    {
        private readonly ICategoryRepository _modelRepository;

        public GetAllCategoriesHandler(ICategoryRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task<IEnumerable<Contract.CoreObjects.Models.Category>> Handle(GetAllCategoriesQuery query)
        {
            return await _modelRepository.GetAll(query.OwnerId);
        }
    }
}
