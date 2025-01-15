using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Business.Handlers.Filters
{
    //TODO : PENSAR EM ALGO PRA CORRIGIR A POSSIBILIDADE DO USUARIO TER O TOKEN DE OUTRO USUARIO E ENVIAR O TOKEN USANDO OUTRO ID DE USER
    public class TokenValidationMiddleware : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var userIdClaim = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.HttpContext.Response.WriteAsync("Usuário não autenticado.");
                return null;
            }

            Guid userIdFromToken = Guid.Parse(userIdClaim.Value);
            Guid? userIdFromRequest = null;
            string body = null;

            if (context.HttpContext.Request.Method == HttpMethods.Post || context.HttpContext.Request.Method == HttpMethods.Put)
            {
                using var reader = new StreamReader(context.HttpContext.Request.Body);
                body = await reader.ReadToEndAsync();

                var requestBody = JsonSerializer.Deserialize<JsonElement>(body);

                if (requestBody.TryGetProperty("UserId", out var userIdElement))
                {
                    userIdFromRequest = Guid.Parse(userIdElement.GetString());
                }

                context.HttpContext.Request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(body));
            }

            if (userIdFromRequest == null && context.HttpContext.Request.RouteValues.TryGetValue("userId", out var routeUserId))
            {
                userIdFromRequest = Guid.Parse(routeUserId.ToString());
            }

            if (userIdFromRequest == null && context.HttpContext.Request.Query.TryGetValue("userId", out var queryUserId))
            {
                userIdFromRequest = Guid.Parse(queryUserId);
            }

            if (userIdFromRequest == null || userIdFromToken != userIdFromRequest)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.HttpContext.Response.WriteAsync("Chave de autenticação inválida.");
                return null;
            }

            if (!string.IsNullOrEmpty(body))
                context.HttpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(body));

            return await next(context);
        }
    }
}
