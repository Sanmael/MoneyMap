using Business.Handlers.Filters;
using Business.Models;
using Business.Response;
using Debts.API.Requests;
using Integrations.Client;
using Integrations.DTO;

namespace Debts.API.EndPoints
{
    public static class DebitsEndPoints
    {
        public static void MapPurchaseEndPoints(this WebApplication app, IConfiguration configuration)
        {
            string cardApiUrl = configuration.GetValue<string>("CardAPIUrl")!;
            string purchaseAPIUrl = configuration.GetValue<string>("PurchaseAPIUrl")!;

            app.MapGet("/GetDebits", async ([AsParameters] GetDebitsRequest getDebitsRequest, AppClient appClient) =>
            {
                BaseResponse<List<PurchaseModel>>? purchasesResponse = await appClient.GetAsync<BaseResponse<List<PurchaseModel>>>(new ClientDTO(Url: $"{purchaseAPIUrl}/GetPurchasesActive?userId={getDebitsRequest.UserId}"));
                BaseResponse<List<CardModel>>? cardsResponse = await appClient.GetAsync<BaseResponse<List<CardModel>>>(new ClientDTO(Url: $"{cardApiUrl}/GetAllPurchaseInInstallmentsListActive?userId={getDebitsRequest.UserId}"));

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


            app.MapGet("/GetDebitsByDate", async ([AsParameters] GetDebitsByDateRequest getDebitsByDateRequest, AppClient appClient) =>
            {
                string parameters = GetParameters(getDebitsByDateRequest.UserId, getDebitsByDateRequest.StartMonth, getDebitsByDateRequest.EndMonth);

                BaseResponse<List<CardModel>>? cardsResponse = await appClient.GetAsync<BaseResponse<List<CardModel>>>(new ClientDTO(Url: $"{cardApiUrl}/{parameters}"));

                BaseResponse<List<PurchaseModel>>? purchasesResponse = default;

                if (getDebitsByDateRequest.StartMonth?.Month == DateTime.Now.Month || getDebitsByDateRequest.StartMonth == null)
                    purchasesResponse = await appClient.GetAsync<BaseResponse<List<PurchaseModel>>>(new ClientDTO(Url: $"{purchaseAPIUrl}/GetPurchasesActive?userId={getDebitsByDateRequest.UserId}"));

                DebitsModel debitsModel = new DebitsModel
                {
                    Purchases = purchasesResponse?.Data,
                    Cards = cardsResponse?.Data
                };

                if (debitsModel.Cards == null || debitsModel.Cards.Count == 0)
                {
                    return Results.NotFound("Não há dívidas");
                }

                return Results.Ok(debitsModel);
            }).
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();
        }

        private static string GetParameters(Guid userId, DateTime? startMonth, DateTime? endMonth)
        {
            if (startMonth == null || endMonth != null)
                return $"GetActiveInstallmentsByMonthAsync?userId={userId}&endMonth={endMonth}";

            if (startMonth != null || endMonth == null)
                return $"GetActiveInstallmentsByMonthAsync?userId={userId}&startMonth={startMonth}";

            return $"GetActiveInstallmentsByMonthAsync?userId={userId}&startMonth={startMonth}&endMonth={endMonth}";
        }
    }
}