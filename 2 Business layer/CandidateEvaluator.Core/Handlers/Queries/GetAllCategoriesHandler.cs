using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Queries;
using CandidateEvaluator.Contract.Repositories;
using CandidateEvaluator.Contract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateEvaluator.Core.Handlers.Queries
{
    public class GetAllCategoriesHandler : IQueryHandler<GetAllCategories, List<Category>>
    {
        private readonly ICategoryRepository _modelRepository;

        public GetAllCategoriesHandler(ICategoryRepository modelRepository)
        {
            this._modelRepository = modelRepository;
        }

        public async Task<List<Category>> HandleAsync(GetAllCategories query)
        {
            return await _modelRepository.GetAll(query.OwnerId);
        }
    }
}
