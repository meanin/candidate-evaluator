using System;
using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Common.Requests.Interview;
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
        public async Task<IActionResult> Create([FromBody] CreateInterviewRequest request)
        {
            var cmd = new CreateInterviewCommand(
                HttpContext.GetUser().Oid, request.Name,
                request.Content.Select(c => (c.CategoryId, c.QuestionCount)).ToList());
            var resultId = await _dispatcher.Send(cmd);
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
        public async Task<IActionResult> Update([FromBody] UpdateInterviewRequest request)
        {
            var cmd = new UpdateInterviewCommand(HttpContext.GetUser().Oid, request.Id, request.Name,
                request.Content.Select(c => (c.CategoryId, c.QuestionCount)).ToList());
            var resultId = await _dispatcher.Send(cmd);
            return Ok(resultId);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _dispatcher.Send(new DeleteInterviewCommand(HttpContext.GetUser().Oid, id));
            return NoContent();
        }
    }
}
