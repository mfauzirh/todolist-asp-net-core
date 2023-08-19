using System.Net;
using System.Text.Json;
using TodoList.Exceptions;
using TodoList.Models;

namespace TodoList.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    private Dictionary<Type, HttpStatusCode> _exceptionStatusCodes = new()
    {
        { typeof(NotFoundException), HttpStatusCode.NotFound },
        { typeof(ConflictException), HttpStatusCode.Conflict },
        { typeof(UnauthorizedAccessException), HttpStatusCode.Unauthorized }
    };

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    public Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        httpContext.Response.ContentType = "application/json";

        if (_exceptionStatusCodes.TryGetValue(ex.GetType(), out HttpStatusCode statusCode))
        {
            httpContext.Response.StatusCode = (int)statusCode;
            var response = new ServiceResponse<object>
            {
                Success = false,
                Message = ex.Message,
            };
            return httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var defaultResponse = new ServiceResponse<object>
        {
            Success = false,
            Message = $"Error Ocurred: {ex.Message}",
        };
        return httpContext.Response.WriteAsync(JsonSerializer.Serialize(defaultResponse));
    }
}