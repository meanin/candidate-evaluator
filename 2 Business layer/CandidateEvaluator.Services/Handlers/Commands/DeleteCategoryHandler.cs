using CandidateEvaluator.Contract.Commands.Category;
using CandidateEvaluator.Contract.Handlers;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateEvaluator.Services.Handlers.Commands
{
    public class DeleteCategoryHandler : ICommandHandler<DeleteCategory>
    {
        private readonly ICategoryService _categoryService;

        public DeleteCategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<Guid> HandleAsync(DeleteCategory command)
        {
            await _categoryService.Delete(command.OwnerId, command.Id);
            return Guid.Empty;
        }
    }
}
