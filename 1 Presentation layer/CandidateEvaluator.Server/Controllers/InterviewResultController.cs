using System;
using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Common.Requests.InterviewResult;
using CandidateEvaluator.Contract.Commands.InterviewResult;
using CandidateEvaluator.Contract.Dispatchers;
using CandidateEvaluator.Contract.Queries.InterviewResult;
using CandidateEvaluator.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CreateCategoryResult = CandidateEvaluator.Contract.Commands.InterviewResult.CreateCategoryResult;
using CreateQuestionResult = CandidateEvaluator.Contract.Commands.InterviewResult.CreateQuestionResult;

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
        public async Task<IActionResult> Create([FromBody] CreateInterviewResultRequest request)
        {
            var user = HttpContext.GetUser();
            var cmd = new CreateInterviewResultCommand(
                user.Oid, 
                request.CandidateName, 
                user.Name, 
                request.InterviewTemplateName, 
                request.InterviewDate,
                request.Content.Select(c => new CreateCategoryResult
                    {
                        CategoryId = c.CategoryId,
                        CategoryName = c.CategoryName,
                        QuestionResults = c.QuestionResults.Select(q => new CreateQuestionResult
                        {
                            QuestionId = q.QuestionId,
                            QuestionName = q.QuestionName,
                            Score = q.Score,
                            Notes = q.Notes
                        }).ToList()
                    }).ToList());
            var resultId = await _dispatcher.Send(cmd);
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
            await _dispatcher.Send(new DeleteInterviewResultCommand(HttpContext.GetUser().Oid, id));
            return NoContent();
        }
    }
}
