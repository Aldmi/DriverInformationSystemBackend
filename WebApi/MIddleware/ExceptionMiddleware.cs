using System.Net;
using System.Text.Json;
using Application.Common.Exceptions;

namespace WebApi.MIddleware;

public class ExceptionMiddleware
{
    readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode code = 0;
        string result = "";
        
        switch (ex)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.Errors);
                break;
            
            case NotFoundException notFoundException:
                code = HttpStatusCode.NotFound;
                break;
            
            case DeleteCommandException deleteCommandException:
                code = HttpStatusCode.Conflict;
                break;
            
            default:
                _logger.LogError($"Неизвестное исключение {ex}");
                break;
        }
        
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        await context.Response.WriteAsync(result);
    }
}


public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder ConfigureCustomExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}