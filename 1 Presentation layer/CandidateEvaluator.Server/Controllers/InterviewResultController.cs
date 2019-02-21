using System;
using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Common.Requests.InterviewResult;
using CandidateEvaluator.Common.Responses.InterviewResult;
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
            var result = await _dispatcher.Query(new GetAllInterviewResultsQuery(HttpContext.GetUser().Oid));
            var response = result.Select(r => new MiniInterviewResultResponse
            {
                Id = r.Id,
                InterviewDate = r.InterviewDate,
                CandidateName = r.CandidateName
            }).ToList();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await _dispatcher.Query(new GetInterviewResultQuery(HttpContext.GetUser().Oid, id));
            var response = new InterviewResultResponse
            {
                Id = result.Id,
                CandidateName = result.CandidateName,
                InterviewDate = result.InterviewDate,
                Content = result.Content.Select(c => new CategoryResultResponse
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    QuestionResults = c.QuestionResults.Select(q => new QuestionResultResponse
                    {
                        QuestionId = q.QuestionId,
                        QuestionName = q.QuestionName,
                        Score = q.Score,
                        Notes = q.Notes
                    }).ToList()
                }).ToList()
            };
            return Ok(response);
        }

        [HttpPost]
        [Route("{id}/sendreport")]
        public async Task<IActionResult> SendReport([FromBody] SendInterviewReportViaMailRequest request)
        {
            await _dispatcher.Send(new SendInterviewReportViaMailCommand(HttpContext.GetUser().Oid, request.Id));
            return Ok();
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
