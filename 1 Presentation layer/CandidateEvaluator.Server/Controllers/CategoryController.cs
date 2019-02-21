using CandidateEvaluator.Contract.Commands.Category;
using CandidateEvaluator.Contract.Dispatchers;
using CandidateEvaluator.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Common.Requests.Category;
using CandidateEvaluator.Common.Responses.Category;
using CandidateEvaluator.Contract.Queries.Category;

namespace CandidateEvaluator.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public CategoryController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
        {
            var cmd = new CreateCategoryCommand(HttpContext.GetUser().Oid, request.Name);
            var created = await _dispatcher.Send(cmd);
            return CreatedAtAction(nameof(Get), created);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _dispatcher.Query(new GetAllCategoriesQuery(HttpContext.GetUser().Oid));
            var response = categories.Select(c => new CategoryResponse
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id) 
        {
            var category = await _dispatcher.Query(new GetCategoryQuery(HttpContext.GetUser().Oid, id));
            return Ok(new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            });
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryRequest request)
        {
            var cmd = new UpdateCategoryCommand(HttpContext.GetUser().Oid, request.Id, request.Name);
            var categoryId = await _dispatcher.Send(cmd);
            return Ok(categoryId);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _dispatcher.Send(new DeleteCategoryCommand(HttpContext.GetUser().Oid, id));
            return NoContent();
        }
    }
}
