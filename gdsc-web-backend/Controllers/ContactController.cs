using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post()
        {
            return StatusCode(201);
        }
        
    }
}