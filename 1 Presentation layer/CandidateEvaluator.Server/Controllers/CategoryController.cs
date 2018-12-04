using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Services;
using CandidateEvaluator.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CandidateEvaluator.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category model)
        {
            model.OwnerId = HttpContext.GetUser().Oid;
            var created = await _service.Add(model);
            return CreatedAtAction(nameof(Get), created);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _service.GetAll(HttpContext.GetUser().Oid);
            return Ok(categories);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var category = await _service.Get(HttpContext.GetUser().Oid, id);
            return Ok(category);
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] Category model)
        {
            model.OwnerId = HttpContext.GetUser().Oid;
            await _service.Update(model);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _service.Delete(HttpContext.GetUser().Oid, id);
            return NoContent();
        }
    }
}
