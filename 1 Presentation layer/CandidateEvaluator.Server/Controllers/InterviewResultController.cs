using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Commands.InterviewResult;
using CandidateEvaluator.Contract.Dispatchers;
using CandidateEvaluator.Contract.Queries.InterviewResult;
using CandidateEvaluator.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CandidateEvaluator.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class InterviewResultController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public InterviewResultController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInterviewResult command)
        {
            var user = HttpContext.GetUser();
            command.OwnerId = user.Oid;
            command.ReviewerName = user.Name;
            var resultId = await _dispatcher.Send(command);
            return CreatedAtAction(nameof(Get), resultId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dispatcher.Query(new GetAllInterviewResults { OwnerId = HttpContext.GetUser().Oid });
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await _dispatcher.Query(new GetInterviewResult { OwnerId = HttpContext.GetUser().Oid, Id = id });
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _dispatcher.Send(new DeleteInterviewResult { OwnerId = HttpContext.GetUser().Oid, Id = id });
            return NoContent();
        }
    }
}
