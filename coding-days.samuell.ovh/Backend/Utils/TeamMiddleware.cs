using System.Linq;
using System.Threading.Tasks;
using CodingDays.Models;
using Microsoft.AspNetCore.Http;

namespace CodingDays.Utils;
public class TeamMiddleware
{
    public TeamMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    private RequestDelegate _next;


    public Task Invoke(HttpContext httpContext, TeamHolder teamHolder, JwtHandler jwtHandler)
    {
        string? bearer = httpContext.Request.Headers["Authorization"].FirstOrDefault();
        teamHolder.TeamId = jwtHandler.ReadTeamIdFromToken(bearer);
        
        return _next.Invoke(httpContext);
    }
}
