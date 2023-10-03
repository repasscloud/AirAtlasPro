using Microsoft.AspNetCore.Mvc;

namespace AirAtlasPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAction()
        {
            return Ok();
        }
    }
}
