using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

//ajustar depois, não salvar logs da api de auth e salvar metodos de get
public class LoggerFilter(ILogger<LoggerFilter> logger) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var httpContext = context.HttpContext;
        var request = httpContext.Request;
        var endpointName = httpContext.GetEndpoint()?.DisplayName ?? "UnknownEndpoint";
        var body = httpContext.Items["RequestBody"] as string;

        var requestData = new
        {
            Method = request.Method,
            Path = request.Path,
            Query = request.Query.Any() ? request.Query.ToDictionary(q => q.Key, q => q.Value.ToString()) : null,
            Body = body
        };

        logger.LogInformation("Handling request to {Endpoint} with data: {@RequestData}", endpointName, requestData);

        Stopwatch stopwatch = Stopwatch.StartNew();

        try
        {
            return await next(context);
        }
        finally
        {
            stopwatch.Stop();
            logger.LogInformation("[Performance] Request to {Endpoint} took {TimeTaken} seconds", endpointName, stopwatch.Elapsed.TotalSeconds);
        }
    }
}
