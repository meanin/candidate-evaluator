using System.Threading.Tasks;
using CandidateEvaluator.Contract.Services;
using CandidateEvaluator.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CandidateEvaluator.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        private readonly IUserRecentActivityService _repository;

        public DashboardController(IUserRecentActivityService repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Get()
        {
            var activities = await _repository.GetAll(HttpContext.GetUser().Oid);
            return Ok(activities);
        }
    }
}
