using System.Linq;
using System.Threading.Tasks;
using CodingDays.UserApi.Models;
using Microsoft.AspNetCore.Http;

namespace CodingDays.UserApi.Utils;
public class AuthMiddleware
{
    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    private RequestDelegate _next;


    public Task Invoke(HttpContext httpContext, AuthHolder authHolder, JwtHandler jwtHandler)
    {
        string? bearer = httpContext.Request.Headers["Authorization"].FirstOrDefault();
        authHolder.AuthId = jwtHandler.ReadIdFromToken(bearer);
        
        return _next.Invoke(httpContext);
    }
}
