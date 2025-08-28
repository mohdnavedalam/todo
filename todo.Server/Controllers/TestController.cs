using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace todo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTest()
        {
            return Ok("test");
        }
    }
}
