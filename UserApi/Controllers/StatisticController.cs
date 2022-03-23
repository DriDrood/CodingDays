using CodingDays.UserApi.Database;
using CodingDays.UserApi.Models.Dto.User;
using Microsoft.AspNetCore.Mvc;

namespace CodingDays.UserApi.Controllers;
[ApiController]
[Route("[controller]")]
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