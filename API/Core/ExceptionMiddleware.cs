
using System.Net;
using System.Text.Json;
using API.Shared;

namespace API.Core;

public class ExceptionMiddleware : IMiddleware
{
    private readonly IWebHostEnvironment env;

    public ExceptionMiddleware(IWebHostEnvironment env)
    {
        this.env = env;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = env.IsDevelopment() ? new APIErrorResponse(context.Response.StatusCode, ex.Message, ex.StackTrace) : new APIErrorResponse(context.Response.StatusCode, ex.Message, "Internal Server Error");

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}
