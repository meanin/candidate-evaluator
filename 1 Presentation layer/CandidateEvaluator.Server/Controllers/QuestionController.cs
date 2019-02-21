using CandidateEvaluator.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Common.Requests.Question;
using CandidateEvaluator.Common.Responses.Question;
using CandidateEvaluator.Contract.CoreObjects.Commands.Question;
using CandidateEvaluator.Contract.CoreObjects.Models;
using CandidateEvaluator.Contract.CoreObjects.Queries.Question;
using CandidateEvaluator.Contract.CQRS.Dispatchers;
using CandidateEvaluator.Contract.Models;

namespace CandidateEvaluator.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public QuestionController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromBody] CreateQuestionRequest request)
        {
            var cmd = new CreateQuestionCommand(
                HttpContext.GetUser().Oid,
                request.Name,
                request.Text,
                request.CategoryId,
                Enum.Parse<QuestionType>(request.Type.ToString())
            );
            var created = await _dispatcher.Send(cmd);
            return CreatedAtAction(nameof(Get), created);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllFromCategory([FromQuery(Name = "categoryid")] Guid categoryId)
        {
            var questions = await _dispatcher.Query(new GetAllQuestionsQuery(HttpContext.GetUser().Oid, categoryId));
            var response = questions.Select(q => new QuestionResponse
            {
                Id = q.Id,
                CategoryId = q.CategoryId,
                Name = q.Name,
                Type = Enum.Parse<Common.Types.QuestionType>(q.Type.ToString()),
                Text = q.Text
            }).ToList();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var question = await _dispatcher.Query(new GetQuestionQuery(HttpContext.GetUser().Oid, id));
            var response = new QuestionResponse
            {
                Id = question.Id,
                CategoryId = question.CategoryId,
                Name = question.Name,
                Type = Enum.Parse<Common.Types.QuestionType>(question.Type.ToString()),
                Text = question.Text
            };
            return Ok(response);
        }

        [HttpPost]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromBody] UpdateQuestionRequest request)
        {
            var cmd = new UpdateQuestionCommand(
                HttpContext.GetUser().Oid,
                request.Id,
                request.Name,
                request.Text,
                request.CategoryId,
                Enum.Parse<QuestionType>(request.Type.ToString())
                );
            var categoryId = await _dispatcher.Send(cmd);
            return Ok(categoryId);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _dispatcher.Send(new DeleteQuestionCommand(HttpContext.GetUser().Oid, id));
            return NoContent();
        }

    }
}