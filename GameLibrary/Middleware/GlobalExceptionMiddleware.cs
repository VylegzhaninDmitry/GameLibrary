using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameLibrary.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception e)
        {
            await HandleExceptionMessageAsync(context, e);
        }
    }
    
    private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)  
    {  
        const int statusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";  
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };
        var result = JsonSerializer.Serialize(new  
        {  
            StatusCode = statusCode,  
            ErrorMessage = exception.Message  
        },options);  
        context.Response.Clear();
        context.Response.ContentType = "application/json";  
        context.Response.StatusCode = statusCode;  
        return context.Response.WriteAsync(result);  
    } 
}