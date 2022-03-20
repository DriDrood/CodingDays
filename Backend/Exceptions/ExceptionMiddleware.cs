using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CodingDays.Exceptions;
public class ExceptionMiddleware
{
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    private RequestDelegate _next;


    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next.Invoke(httpContext);
        }
        catch (UsageException ex)
        {
            await WriteException(httpContext, HttpStatusCode.BadRequest, ex);
        }
        catch (Exception ex)
        {
            await WriteException(httpContext, HttpStatusCode.InternalServerError, ex);
        }
    }

    private Task WriteException(HttpContext httpContext, HttpStatusCode statusCode, Exception exception)
    {
        httpContext.Response.StatusCode = (int)statusCode;

        byte[] bodyAsByte = Encoding.UTF8.GetBytes($"{{\"message\":\"{exception.Message}\"}}");
        return httpContext.Response.Body.WriteAsync(bodyAsByte, 0, bodyAsByte.Length);
    }
}
