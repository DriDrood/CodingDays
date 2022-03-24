using Microsoft.AspNetCore.Mvc;

namespace TempApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UtilsController : ControllerBase
{
    [HttpGet("GetCorrectFilename")]
    public ActionResult GetCorrectFilename()
    {
        return Ok("featureNotBug.exe");
    }
}
