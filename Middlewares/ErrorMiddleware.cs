using NobelTaskForInterview.Models;
using System.Net;

namespace NobelTaskForInterview.Middlewares;

public class ErrorMiddleware
{
    private readonly ILogger _logger;
    private readonly RequestDelegate _next;

    public ErrorMiddleware(ILogger<ErrorMiddleware> loger, RequestDelegate next)
    {
        _logger = loger;
        _next = next;
    }

    public async Task Invoke(HttpContext http)
    {
        try
        {
            await _next(http);
        }
        catch (NobelException ex)
        {
            _logger.LogWarning(ex.Message);
            http.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await http.Response.WriteAsync(ex.Message);
        }
        catch (Exception ex)
        { 
            _logger.LogError(ex.Message);
        }
    }
}
