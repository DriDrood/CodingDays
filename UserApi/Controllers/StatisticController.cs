using System.Linq;
using CodingDays.UserApi.Database;
using CodingDays.UserApi.Models.Dto.Statistic;
using Microsoft.AspNetCore.Mvc;

namespace CodingDays.UserApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class StatisticController : ControllerBase
{
    public StatisticController(DB db)
    {
        _db = db;
    }

    private readonly DB _db;

    [HttpGet("[action]")]
    public GetCountResp GetUserCount()
    {
        int count = _db.Users.Count();

        return new GetCountResp(count);
    }
}