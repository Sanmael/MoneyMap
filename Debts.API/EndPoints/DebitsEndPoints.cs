using Business.Handlers.Filters;
using Business.Models;
using Business.Response;
using Integrations.Client;
using Integrations.DTO;

namespace Debts.API.EndPoints
{
    public static class DebitsEndPoints
    {        
        public static void MapPurchaseEndPoints(this WebApplication app)
        {
            app.MapGet("/GetDebits", async (Guid userId, AppClient appClient) =>
            {
                BaseResponse<List<PurchaseModel>> purchasesResponse = await appClient.GetAsync<BaseResponse<List<PurchaseModel>>>(new ClientDTO(Url: $"http://localhost:5210/GetPurchasesActive?userId={userId}"));
                BaseResponse<List<CardModel>> cardsResponse = await appClient.GetAsync<BaseResponse<List<CardModel>>>(new ClientDTO(Url: $"http://localhost:5059/GetAllPurchaseInInstallmentsListActive?userId={userId}"));

                DebitsModel debitsModel = new DebitsModel
                {
                    Purchases = purchasesResponse?.Data,
                    Cards = cardsResponse?.Data
                };

                if ((debitsModel.Purchases == null || debitsModel.Purchases.Count == 0) &&
                    (debitsModel.Cards == null || debitsModel.Cards.Count == 0))
                {
                    return Results.NotFound("Não há dívidas");
                }

                return Results.Ok(debitsModel);
            }).
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();

                
            //app.MapGet("/GetDebits", async (DateTime firstDate, DateTime lastDate,Guid userId, AppClient appClient) =>
            //{
            //    BaseResponse<List<PurchaseModel>> purchasesResponse = await appClient.GetAsync<BaseResponse<List<PurchaseModel>>>(new ClientDTO(Url: $"http://localhost:5210/GetPurchasesActive?userId={userId}"));
            //    BaseResponse<List<CardModel>> cardsResponse = await appClient.GetAsync<BaseResponse<List<CardModel>>>(new ClientDTO(Url: $"http://localhost:5059/GetAllPurchaseInInstallmentsListActive?userId={userId}"));

            //    DebitsModel debitsModel = new DebitsModel
            //    {
            //        Purchases = purchasesResponse?.Result,
            //        Cards = cardsResponse?.Result
            //    };

            //    if ((debitsModel.Purchases == null || debitsModel.Purchases.Count == 0) &&
            //        (debitsModel.Cards == null || debitsModel.Cards.Count == 0))
            //    {
            //        return Results.NotFound("Não há dívidas");
            //    }

            //    return Results.Ok(debitsModel);
            //}).
            //AddEndpointFilter<LoggerFilter>().
            //AddEndpointFilter<ValidationFilter>();
        }
    }
}