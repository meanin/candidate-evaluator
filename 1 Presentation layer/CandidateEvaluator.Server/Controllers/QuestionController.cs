using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Services;
using CandidateEvaluator.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CandidateEvaluator.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private IQuestionService _service;

        public QuestionController(IQuestionService questionService)
        {
            _service = questionService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Question model)
        {
            model.OwnerId = HttpContext.GetUser().Oid;
            var created = await _service.Add(model);
            return CreatedAtAction(nameof(Create), created);
        }
    }
}