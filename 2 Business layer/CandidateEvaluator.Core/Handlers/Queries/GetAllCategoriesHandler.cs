using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Queries;
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
        private readonly ICategoryService _categoryService;

        public GetAllCategoriesHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<List<Category>> HandleAsync(GetAllCategories query)
        {
            return await _categoryService.GetAll(query.OwnerId);
        }
    }
}
