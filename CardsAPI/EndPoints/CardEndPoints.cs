using Business.Handlers.Filters;
using Business.Interfaces;
using Business.Requests.Card;
using Business.Response;
using Microsoft.AspNetCore.Authorization;

namespace CardsAPI.EndPoints
{
    public static class CardEndPoints
    {
        [Authorize]
        public static void MapCardEndPoints(this WebApplication app)
        {
            app.MapPost("/GenerateCard", async (InsertCardRequest request, ICardService cardService) =>
            {
                BaseResponse response = await cardService.AddCardAsync(request);

                if (!response.Success)
                    return Results.BadRequest(response);

                response.Location = $"/GetCard?userId={request.UserId}&cardId={response.GetData<Guid>()}";

                return Results.Created(response.Location, response);
            }).
            RequireAuthorization().
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();

            app.MapGet("/GetCard", async ([AsParameters] GetCardRequest request, ICardService cardService) =>
            {
                BaseResponse response = await cardService.GetCardAsync(request);

                if (!response.Success)
                    return Results.NotFound(response);

                return Results.Ok(response);
            }).
            RequireAuthorization().
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();
        }
    }
}