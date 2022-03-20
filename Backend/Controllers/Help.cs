using System;
using System.Linq;
using CodingDays.Database;
using CodingDays.Database.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CodingDays.Controllers
{
    public class HelpController : ControllerBase
    {
        public HelpController(DB db)
        {
            _db = db;
        }

        private readonly DB _db;
    }
}