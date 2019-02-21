using System;
using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Common.Requests.Interview;
using CandidateEvaluator.Common.Responses.Interview;
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
            var result = await _dispatcher.Query(new GetAllInterviewsQuery(HttpContext.GetUser().Oid));
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await _dispatcher.Query(new GetInterviewQuery(HttpContext.GetUser().Oid, id));
            var response = new InterviewResponse
            {
                Id = result.Id,
                Name = result.Name,
                Content = result.Content.Select(c => new InterviewContentResponse
                {
                    CategoryId = c.Category.Id,
                    QuestionCount = c.Questions.Count
                }).ToList()
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}/start")]
        public async Task<IActionResult> Start([FromRoute] Guid id)
        {
            var result = await _dispatcher.Query(new GetInterviewQuery(HttpContext.GetUser().Oid, id));
            var response = new StartInterviewResponse
            {
                Id = result.Id,
                Name = result.Name,
                Content = result.Content.Select(c => new StartInterviewContentResponse
                {
                    Id = c.Category.Id,
                    Name = c.Category.Name,
                    Questions = c.Questions.Select(q => new StartInterviewQuestionResponse
                    {
                        Id = q.Id,
                        Name = q.Name,
                        Type = q.Type.ToString(),
                        Text = q.Text
                    }).ToList()
                }).ToList()
            };
            return Ok(response);
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
