using Business.Interfaces;
using Business.Response;
using Business.Security;
using System.Security.Claims;

namespace UserAPI.EndPoints
{
    public static class AuthenticationEndPoint
    {
        public static void MapperAuthenticationEndPoint(this WebApplication app)
        {
            app.MapPost("/Login", async (IAuthenticationService authService, LoginRequest login) =>
            {
                BaseResponse result = await authService.LoginAsync(login.UserName, login.Password);

                if (result.Success)
                    return Results.Ok(result);

                return Results.BadRequest(result.Message);
            }).
            AddEndpointFilter<ValidationFilter>();

            app.MapGet("/GetUser", (HttpContext context) =>
            {
                string token = TokenService.GetAccessToken(context);
                ClaimsPrincipal claimsPrincipal = TokenService.ReadJsonJWT(token);

                var objeto = new
                {
                    Nome = claimsPrincipal.Identity!.Name,
                    Id = claimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                };

                return Results.Ok(objeto);
            }).
             RequireAuthorization().
             AddEndpointFilter<ValidationFilter>();

            app.MapPost("/Logout", async (ClaimsPrincipal claimsPrincipal, IAuthenticationService _authService) =>
            {
                Guid userId = Guid.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var db = await _authService.LogOut(userId);
                return Results.Ok("Logout bem-sucedido.");
            }).
             RequireAuthorization().
             AddEndpointFilter<ValidationFilter>();
        }
    }
}