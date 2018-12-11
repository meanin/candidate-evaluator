using CandidateEvaluator.Contract.Commands.Category;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Services;
using System;
using System.Threading.Tasks;

namespace CandidateEvaluator.Core.Handlers.Commands
{
    public class UpdateCategoryHandler : ICommandHandler<UpdateCategory>
    {
        private readonly ICategoryService _categoryService;

        public UpdateCategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<Guid> HandleAsync(UpdateCategory command)
        {
            var category = new Category
            {
                Id = command.Id,
                OwnerId = command.OwnerId,
                Name = command.Name
            };
            return await _categoryService.Update(category);
        }
    }
}
