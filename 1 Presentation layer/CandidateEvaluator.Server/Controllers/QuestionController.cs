
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
        public async Task<IActionResult> Create([FromBody] CreateQuestion command)
        {
            command.OwnerId = HttpContext.GetUser().Oid;
            var created = await _dispatcher.SendAsync(command);
            return CreatedAtAction(nameof(Create), created);
        }

        [HttpGet("{categoryId:guid}")]
        public async Task<IActionResult> GetAllFromCategory(Guid categoryId)
        {
            var questions = await _dispatcher.QueryAsync(new GetQuestionsFromCategory { OwnerId = HttpContext.GetUser().Oid, CategoryId = categoryId });
            return Ok(questions);
        }

        [HttpGet("{categoryId:guid}/{id:guid}")]
        public async Task<IActionResult> Get(Guid categoryId, Guid id)
        {
            var question = await _dispatcher.QueryAsync(new GetQuestion { OwnerId = HttpContext.GetUser().Oid, CategoryId = categoryId, Id = id });
            return Ok(question);
        }


        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateQuestion command)
        {
            command.OwnerId = HttpContext.GetUser().Oid;
            var categoryId = await _dispatcher.SendAsync(command);
            return Ok(categoryId);
        }

        [HttpDelete]
        [Route("{categoryId:guid}/{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid categoryId, [FromRoute] Guid id)
        {
            await _dispatcher.SendAsync(new DeleteQuestion { OwnerId = HttpContext.GetUser().Oid, CategoryId = categoryId, Id = id });
            return NoContent();
        }

    }
}