using Microsoft.AspNetCore.Mvc;

namespace CodingDays.Controllers
{
    public class RegisterController : ControllerBase
    {
        [HttpPost]
        public ActionResult Register()
        {
            return Redirect("/done.html");
        }
    }
}