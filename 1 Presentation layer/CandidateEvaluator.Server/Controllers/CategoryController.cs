using CandidateEvaluator.Contract.Commands.Category;
using CandidateEvaluator.Contract.Dispatchers;
using CandidateEvaluator.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Create([FromBody] CreateCategory command)
        {
            command.OwnerId = HttpContext.GetUser().Oid;
            var created = await _dispatcher.Send(command);
            return CreatedAtAction(nameof(Get), created);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _dispatcher.Query(new GetAllCategories { OwnerId = HttpContext.GetUser().Oid });
            return Ok(categories);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id) 
        {
            var category = await _dispatcher.Query(new GetCategory { OwnerId = HttpContext.GetUser().Oid, Id = id });
            return Ok(category);
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateCategory command)
        {
            command.OwnerId = HttpContext.GetUser().Oid;
            var categoryId = await _dispatcher.Send(command);
            return Ok(categoryId);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _dispatcher.Send(new DeleteCategory { OwnerId = HttpContext.GetUser().Oid, Id = id });
            return NoContent();
        }
    }
}
