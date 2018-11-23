using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CandidateEvaluator.Server.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}
