using Business.Handlers.Filters;
using Business.Models;
using Business.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Debts.API.EndPoints
{
    public static class DebitsEndPoints
    {
        //TODO : REFATORAR TODO O METODO
        public static void MapPurchaseEndPoints(this WebApplication app)
        {
            app.MapGet("/GetDebits", async (Guid userId, [FromServices] IHttpClientFactory httpClientFactory) =>
            {
                var client = httpClientFactory.CreateClient();

                using var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5210/GetPurchasesActive?userId={userId}");
                using var response = await client.SendAsync(request);

                BaseResponse<List<PurchaseModel>>? basePurchaseModelResponse = default;
                BaseResponse<List<CardModel>>? baseCardModelResponse = default;

                if (response.StatusCode == HttpStatusCode.OK)
                    basePurchaseModelResponse = await response.Content.ReadFromJsonAsync<BaseResponse<List<PurchaseModel>>>();

                using var cardRequest = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5059/GetAllPurchaseInInstallmentsListActive?userId={userId}");
                using var responseCard = await client.SendAsync(cardRequest);

                if (responseCard.StatusCode == HttpStatusCode.OK)
                    baseCardModelResponse = await responseCard.Content.ReadFromJsonAsync<BaseResponse<List<CardModel>>>();

                DebitsModel debitsModel = new DebitsModel()
                {                    
                    Cards = baseCardModelResponse != null ? baseCardModelResponse.Result : null,
                    Purchases = basePurchaseModelResponse != null ? basePurchaseModelResponse.Result : null
                };

                if (debitsModel.Purchases == null && debitsModel.Cards == null || debitsModel?.Purchases?.Count == 0 && debitsModel?.Cards?.Count == 0)
                    return Results.NotFound("Não há dividas");

                return Results.Ok(debitsModel);
            }).
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();
        }
    }
}