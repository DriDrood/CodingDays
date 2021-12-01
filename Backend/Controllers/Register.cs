using Microsoft.AspNetCore.Mvc;

namespace CodingDays.Controllers
{
    public class RegisterController : ControllerBase
    {
        public ActionResult Get()
        {
            return Ok("Ahoj");
        }

        [HttpPost]
        public ActionResult Register()
        {
            return Redirect("/done.html");
        }
    }
}