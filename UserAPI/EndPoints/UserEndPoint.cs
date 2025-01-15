using Business.Handlers.Filters;
using Business.Interfaces;
using Business.Requests.User;
using Business.Response;

namespace UserAPI.EndPoints
{
    public static class UserEndPoint
    {
        public static void MapperUserEndPoint(this WebApplication app)
        {
            app.MapPost("/AddUser", async (CreateUserRequest user, IUserService authService) =>
            {
                BaseResponse result = await authService.SaveNewUser(user);

                if (result.Success)
                    return Results.Created("Usuário criado com sucesso.", "");

                return Results.BadRequest(result.Message);
            }).
             AddEndpointFilter<LoggerFilter>().
             AddEndpointFilter<ValidationFilter>();            
        }
    }
}