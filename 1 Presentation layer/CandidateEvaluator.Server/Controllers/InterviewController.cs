using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Commands.Interview;
using CandidateEvaluator.Contract.Dispatchers;
using CandidateEvaluator.Contract.Queries.Category;
using CandidateEvaluator.Contract.Queries.Interview;
using CandidateEvaluator.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CandidateEvaluator.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class InterviewController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public InterviewController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInterview command)
        {
            command.OwnerId = HttpContext.GetUser().Oid;
            var created = await _dispatcher.SendAsync(command);
            return CreatedAtAction(nameof(Get), created);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _dispatcher.QueryAsync(new GetAllCategories { OwnerId = HttpContext.GetUser().Oid });
            return Ok(categories);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await _dispatcher.QueryAsync(new GetInterview { OwnerId = HttpContext.GetUser().Oid, Id = id });
            return Ok(result);
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateInterview command)
        {
            command.OwnerId = HttpContext.GetUser().Oid;
            var resultId = await _dispatcher.SendAsync(command);
            return Ok(resultId);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _dispatcher.SendAsync(new DeleteInterview { OwnerId = HttpContext.GetUser().Oid, Id = id });
            return NoContent();
        }
    }
}
