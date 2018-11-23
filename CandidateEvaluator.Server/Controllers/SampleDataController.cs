using Microsoft.AspNetCore.Mvc;

namespace CandidateEvaluator.Server.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
