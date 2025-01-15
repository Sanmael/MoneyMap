using Business.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Business.Handlers.Filters
{
    public class LoggerFilter(ILogger<BaseRequest> logger) : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {           
            if (context.Arguments.FirstOrDefault(x => typeof(BaseRequest).IsAssignableFrom(x.GetType())) is not BaseRequest request)
                return await next(context);

            var endpointName = context.HttpContext.GetEndpoint()?.DisplayName ?? request.GetType().Name;

            logger.LogInformation("{@Handler}.Handle called with {@Query}", endpointName, request.GetSerializedRequest());

            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                return await next(context);
            }

            finally
            {
                stopwatch.Stop();
                logger.LogInformation("[Performance] the request {Request} took {TimeTaken}", endpointName, stopwatch.Elapsed.TotalSeconds);
            }
        }
    }
}