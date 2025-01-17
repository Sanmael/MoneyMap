using Business.DTOS;
using Business.Interfaces;
using Business.Requests.Card.PurchaseInInstallments;
using Business.Response;
using Business.Security;

namespace CardsAPI.EndPoints
{
    public static class PurchaseInInstallmentsEndPoints
    {
        public static void MapPurchaseInInstallmentsEndPoints(this WebApplication app)
        {
            app.MapPost("/AddPurchaseInInstallments", async (InsertPurchaseInInstallmentsRequest request, IPurchaseInInstallmentsService service, HttpContext context) =>
            {
                ClaimsDTO claimsDTO = TokenService.GetUserData(context);
                request.UserId = claimsDTO.UserId;

                BaseResponse response = await service.AddPurchaseInInstallmentsAsync(request);

                if (!response.Success)
                    return Results.BadRequest(response);

                response.Location = $"/GetPurchaseInInstallments?userId={request.UserId}&purchaseInInstallmentsId={response.GetData<Guid>()}";

                return Results.Created(response.Location, response);
            }).
            RequireAuthorization().
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();

            app.MapGet("/GetPurchaseInInstallments", async (Guid purchaseInInstallmentsId, IPurchaseInInstallmentsService service, HttpContext context) =>
            {
                ClaimsDTO claimsDTO = TokenService.GetUserData(context);

                GetPurchaseInInstallmentsRequest request = new GetPurchaseInInstallmentsRequest()
                {
                    PurchaseInInstallmentsId = purchaseInInstallmentsId,
                    UserId = claimsDTO.UserId
                };

                BaseResponse response = await service.GetPurchaseInInstallments(request);

                if (!response.Success)
                    return Results.NotFound(response);

                return Results.Ok(response);
            }).
            RequireAuthorization().
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();

            app.MapGet("/GetPurchaseInInstallmentsListActiveByCardId", async (Guid cardId, IPurchaseInInstallmentsService service, HttpContext context) =>
            {
                ClaimsDTO claimsDTO = TokenService.GetUserData(context);

                GetPurchaseInInstallmentsListActiveRequest request = new GetPurchaseInInstallmentsListActiveRequest()
                {
                    CardId = cardId,
                    UserId = claimsDTO.UserId
                };

                BaseResponse response = await service.GetPurchaseInInstallmentsListActive(request);

                if (!response.Success)
                    return Results.NotFound(response);

                return Results.Ok(response);
            }).
            RequireAuthorization().
            AddEndpointFilter<LoggerFilter>(). 
            AddEndpointFilter<ValidationFilter>();

            app.MapGet("/GetAllPurchaseInInstallmentsListActive", async (IPurchaseInInstallmentsService service, HttpContext context) =>
            {
                ClaimsDTO claimsDTO = TokenService.GetUserData(context);

                GetAllPurchaseInInstallmentsListActiveRequest request = new GetAllPurchaseInInstallmentsListActiveRequest()
                {
                    UserId = claimsDTO.UserId
                };

                BaseResponse response = await service.GetAllPurchaseInInstallmentsListActive(request);

                if (!response.Success)
                    return Results.NotFound(response);

                return Results.Ok(response);
            }).
            RequireAuthorization().
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();

            app.MapGet("/GetAllPurchaseInInstallmentsListActiveByDate", async (DateTime firstDate, DateTime lastDate, IPurchaseInInstallmentsService service, HttpContext context) =>
            {
                ClaimsDTO claimsDTO = TokenService.GetUserData(context);

                GetAllPurchaseInInstallmentsListActiveByDateRequest request = new GetAllPurchaseInInstallmentsListActiveByDateRequest()
                {
                    FirstDate = firstDate,
                    LastDate = lastDate,
                    UserId = claimsDTO.UserId
                };

                BaseResponse response = await service.GetAllPurchaseInInstallmentsListActiveByDate(request);

                if (!response.Success)
                    return Results.NotFound(response);

                return Results.Ok(response);
            }).
            RequireAuthorization().
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();

            app.MapGet("/GetActiveInstallmentsByMonthAsync", async (DateTime startMonth, DateTime endMonth, IPurchaseInInstallmentsService service, HttpContext context) =>
            {
                ClaimsDTO claimsDTO = TokenService.GetUserData(context);

                GetActiveInstallmentsByMonthAsyncRequest request = new GetActiveInstallmentsByMonthAsyncRequest()
                {
                    StartMonth = startMonth,
                    EndMonth = endMonth,
                    UserId = claimsDTO.UserId
                };

                BaseResponse response = await service.GetActiveInstallmentsByMonthAsync(request);

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