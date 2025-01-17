using Microsoft.AspNetCore.Http;

public class RequestBodyMiddleware
{
    private readonly RequestDelegate _next;

    public RequestBodyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.EnableBuffering();

        using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        
        context.Items["RequestBody"] = body;

        context.Request.Body.Position = 0;
        await _next(context);
    }
}