using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Business.Handlers
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        private const string UnhandledExceptionMsg = "An unhandled exception has occurred while executing the request.";

        public CustomExceptionHandler()
        {
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            httpContext.Response.ContentType = "application/json";

            switch (exception)
            {
                default:
                    await MapperExceptionAsync(httpContext);
                    break;
            }

            return true;
        }

        private async Task MapperExceptionAsync(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(UnhandledExceptionMsg);
        }
    }
}