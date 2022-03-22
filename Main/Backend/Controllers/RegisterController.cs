using System.Linq;
using CodingDays.Database;
using CodingDays.Database.Entities;
using CodingDays.Models.Dto.Registration;
using Microsoft.AspNetCore.Mvc;

namespace CodingDays.Controllers
{
    public class RegisterController : ControllerBase
    {
        public RegisterController(DB db)
        {
            _db = db;
        }

        private readonly DB _db;

        [HttpPost]
        public ActionResult Register([FromBody] RegistrationReq registration)
        {
            Registration reg = new Registration(registration);

            _db.Add(reg);
            _db.SaveChanges();

            return Ok(reg.Id);
        }

        [HttpGet]
        public CountResp Count()
        {
            int registrationCount = _db.Registrations.Count();

            return new CountResp(registrationCount);
        }

        [HttpGet]
        public ActionResult ListNoobs()
        {
            Registration[] registrations = _db.Registrations.ToArray();
            return Ok(registrations);
        }
    }
}