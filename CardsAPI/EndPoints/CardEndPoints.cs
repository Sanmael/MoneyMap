using Azure.Core;
using Business.DTOS;
using Business.Interfaces;
using Business.Requests.Card;
using Business.Response;
using Business.Security;
using Microsoft.AspNetCore.Authorization;

namespace CardsAPI.EndPoints
{
    public static class CardEndPoints
    {
        [Authorize]
        public static void MapCardEndPoints(this WebApplication app)
        {
            app.MapPost("/GenerateCard", async (InsertCardRequest request, ICardService cardService, HttpContext context) =>
            {
                ClaimsDTO claimsDTO = TokenService.GetUserData(context);
                request.UserId = claimsDTO.UserId;

                BaseResponse response = await cardService.AddCardAsync(request);

                if (!response.Success)
                    return Results.BadRequest(response);

                response.Location = $"/GetCard?userId={request.UserId}&cardId={response.GetData<Guid>()}";

                    return Results.Created(response.Location, response);
            }).
            RequireAuthorization().
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();

            app.MapGet("/GetCard", async (Guid cardId, ICardService cardService, HttpContext context) =>
            {
                ClaimsDTO claimsDTO = TokenService.GetUserData(context);

                GetCardRequest request = new GetCardRequest()
                {
                    CardId = cardId,
                    UserId = claimsDTO.UserId
                };

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