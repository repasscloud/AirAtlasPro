using Microsoft.AspNetCore.Mvc;

namespace AirAtlasPro.Controllers
{
    /// <summary>
    /// Test Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAction()
        {
            return Ok();
        }
    }
}
