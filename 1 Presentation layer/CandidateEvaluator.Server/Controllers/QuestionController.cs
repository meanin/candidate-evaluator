using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Services;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ownerId = HttpContext.GetUser().Oid;
            var allQuestions = await _service.GetAll(ownerId);

            return CreatedAtAction(nameof(GetAll), allQuestions);
        }

        [HttpGet("{categoryId:guid}")]
        public async Task<IActionResult> GetAllFromCategory(Guid categoryId)
        {
            var ownerId = HttpContext.GetUser().Oid;
            var questionsFromCategory = await _service.GetAllFromCategory(ownerId, categoryId);

            return CreatedAtAction(nameof(GetAllFromCategory), questionsFromCategory);
        }

        [HttpGet("/{ownerId:guid}/{categoryId:guid}/{id:guid}")]
        public async Task<IActionResult> Get(Guid ownerId, Guid categoryId, Guid id)
        {
            var entity = await _service.Get(ownerId, categoryId, id);
            return Ok(entity);
        }
    }
}