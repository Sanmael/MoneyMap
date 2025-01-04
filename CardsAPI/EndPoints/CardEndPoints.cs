using Business.Handlers.Filters;
using Business.Interfaces;
using Business.Requests.Card;
using Business.Response;

namespace CardsAPI.EndPoints
{
    public static class CardEndPoints
    {
        public static void MapCardEndPoints(this WebApplication app)
        {
            app.MapPost("/GenerateCard", async (InsertCardRequest request, ICardService cardService) =>
            {
                BaseResponse response = await cardService.AddCardAsync(request);

                if (!response.Success)
                    return Results.BadRequest(response);

                return Results.Ok(response);

            }).
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();

            app.MapGet("/GetCard", async ([AsParameters] GetCardRequest request, ICardService cardService) =>
            {
                BaseResponse response = await cardService.GetCardAsync(request);

                if (!response.Success)
                    return Results.BadRequest(response);

                return Results.Ok(response);

            }).
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();
        }
    }
}