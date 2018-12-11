using System.Threading.Tasks;
using CandidateEvaluator.Contract.Dispatchers;
using CandidateEvaluator.Contract.Queries.UserActivity;
using CandidateEvaluator.Contract.Services;
using CandidateEvaluator.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CandidateEvaluator.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public DashboardController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task<IActionResult> Get()
        {
            var activities = await _dispatcher.QueryAsync(new GetAllUserActivities { OwnerId = HttpContext.GetUser().Oid });
            return Ok(activities);
        }
    }
}
