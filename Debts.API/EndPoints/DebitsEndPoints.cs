using Business.Handlers.Filters;
using Business.Models;
using Business.Response;
using Debts.API.Requests;
using Domain.Interfaces.Repositories;
using Integrations.Client;
using Integrations.DTO;
using System.Security.Claims;
namespace Debts.API.EndPoints
{
    public static class DebitsEndPoints
    {
        public static void MapPurchaseEndPoints(this WebApplication app, IConfiguration configuration)
        {
            string cardApiUrl = configuration.GetValue<string>("CardAPIUrl")!;
            string purchaseAPIUrl = configuration.GetValue<string>("PurchaseAPIUrl")!;

            app.MapGet("/GetDebits", async ([AsParameters] GetDebitsRequest getDebitsRequest, IAuthenticationRepositorie iAuthenticationRepositorie, AppClient appClient, ClaimsPrincipal claimsPrincipal) =>
            {
                string token = await iAuthenticationRepositorie.GetAuthenticationToken(getDebitsRequest.UserId);

                BaseResponse<List<PurchaseModel>>? purchasesResponse = await appClient.GetAsync<BaseResponse<List<PurchaseModel>>>(new ClientDTO(Url: $"{purchaseAPIUrl}/GetPurchasesActive?userId={getDebitsRequest.UserId}", Token: token));
                BaseResponse<List<CardModel>>? cardsResponse = await appClient.GetAsync<BaseResponse<List<CardModel>>>(new ClientDTO(Url: $"{cardApiUrl}/GetAllPurchaseInInstallmentsListActive?userId={getDebitsRequest.UserId}", Token: token));

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
            AddEndpointFilter<ValidationFilter>().
            AddEndpointFilter<TokenValidationMiddleware>().
            RequireAuthorization();


            app.MapGet("/GetDebitsByDate", async ([AsParameters] GetDebitsByDateRequest getDebitsByDateRequest, IAuthenticationRepositorie iAuthenticationRepositorie, AppClient appClient) =>
            {
                string token = await iAuthenticationRepositorie.GetAuthenticationToken(getDebitsByDateRequest.UserId);

                string parameters = GetParameters(getDebitsByDateRequest.UserId, getDebitsByDateRequest.StartMonth, getDebitsByDateRequest.EndMonth);

                BaseResponse<List<CardModel>>? cardsResponse = await appClient.GetAsync<BaseResponse<List<CardModel>>>(new ClientDTO(Url: $"{cardApiUrl}/{parameters}", Token: token));

                BaseResponse<List<PurchaseModel>>? purchasesResponse = default;

                if (getDebitsByDateRequest.StartMonth?.Month == DateTime.Now.Month || getDebitsByDateRequest.StartMonth == null)
                    purchasesResponse = await appClient.GetAsync<BaseResponse<List<PurchaseModel>>>(new ClientDTO(Url: $"{purchaseAPIUrl}/GetPurchasesActive?userId={getDebitsByDateRequest.UserId}", Token: token));

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
            AddEndpointFilter<ValidationFilter>().
            RequireAuthorization();

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