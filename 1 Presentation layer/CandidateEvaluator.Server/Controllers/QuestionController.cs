
using CandidateEvaluator.Contract.Commands.Question;
using CandidateEvaluator.Contract.Dispatchers;
using CandidateEvaluator.Contract.Queries.Question;
using CandidateEvaluator.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CandidateEvaluator.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private IDispatcher _dispatcher;

        public QuestionController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromBody] CreateQuestion command)
        {
            command.OwnerId = HttpContext.GetUser().Oid;
            var created = await _dispatcher.Send(command);
            return CreatedAtAction(nameof(Get), created);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllFromCategory([FromQuery(Name = "categoryid")] Guid categoryId)
        {
            var questions = await _dispatcher.Query(new GetQuestions
            {
                OwnerId = HttpContext.GetUser().Oid,
                CategoryId = categoryId
            });

            return Ok(questions);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var question = await _dispatcher.Query(new GetQuestion
            {
                OwnerId = HttpContext.GetUser().Oid,
                Id = id
            });
            return Ok(question);
        }


        [HttpPost]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromBody] UpdateQuestion command)
        {
            command.OwnerId = HttpContext.GetUser().Oid;
            var categoryId = await _dispatcher.Send(command);
            return Ok(categoryId);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _dispatcher.Send(new DeleteQuestion
            {
                OwnerId = HttpContext.GetUser().Oid,
                Id = id
            });
            return NoContent();
        }

    }
}