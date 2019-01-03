using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Commands.Interview;
using CandidateEvaluator.Contract.Dispatchers;
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
            var resultId = await _dispatcher.Send(command);
            return CreatedAtAction(nameof(Get), resultId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dispatcher.Query(new GetAllInterviews { OwnerId = HttpContext.GetUser().Oid });
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await _dispatcher.Query(new GetInterview { OwnerId = HttpContext.GetUser().Oid, Id = id });
            return Ok(result);
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateInterview command)
        {
            command.OwnerId = HttpContext.GetUser().Oid;
            var resultId = await _dispatcher.Send(command);
            return Ok(resultId);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _dispatcher.Send(new DeleteInterview { OwnerId = HttpContext.GetUser().Oid, Id = id });
            return NoContent();
        }
    }
}
