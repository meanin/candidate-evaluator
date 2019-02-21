using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Common.Responses.Dashboard;
using CandidateEvaluator.Contract.Dispatchers;
using CandidateEvaluator.Contract.Queries.UserActivity;
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
            var activities = await _dispatcher.Query(new GetAllUserActivitiesQuery(HttpContext.GetUser().Oid));
            var response = activities.Select(a => new RecentActivityResponse
            {
                EntityId = a.EntityId,
                Name = a.Name,
                Type = a.Type.ToString()
            }).ToList();
            return Ok(response);
        }
    }
}
