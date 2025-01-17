using Business.DTOS;
using Business.Interfaces;
using Business.Requests.Purchase;
using Business.Response;
using Business.Security;

namespace PurchaseAPI.EndPoints
{
    public static class PurchaseEndPoints
    {
        public static void MapPurchaseEndPoints(this WebApplication app)
        {
            app.MapPost("/AddPurchase", async (InsertPurchaseRequest request, IPurchaseService service, HttpContext context) =>
            {
                ClaimsDTO claimsDTO = TokenService.GetUserData(context);
                request.UserId = claimsDTO.UserId;

                BaseResponse response = await service.AddPurchaseAsync(request);

                if (!response.Success)
                    return Results.BadRequest(response);

                response.Location = $"/GetPurchase?userId={request.UserId}&purchaseId={response.GetData<Guid>()}";

                return Results.Created(response.Location, response);
            }).
            RequireAuthorization().
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();

            app.MapGet("/GetPurchase", async (Guid purchaseId, IPurchaseService service, HttpContext context) =>
            {                
                ClaimsDTO claimsDTO = TokenService.GetUserData(context);

                GetPurchaseRequest request = new GetPurchaseRequest()
                {
                    UserId = claimsDTO.UserId,
                    PurchaseId = purchaseId
                };
                
                BaseResponse response = await service.GetPurchase(request);

                if (!response.Success)
                    return Results.NotFound(response);

                return Results.Ok(response);
            }).
            RequireAuthorization().
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();

            app.MapGet("/GetPurchasesActive", async (IPurchaseService service, HttpContext context) =>
            {
                ClaimsDTO claimsDTO = TokenService.GetUserData(context);

                GetPurchaseListActiveRequest request = new GetPurchaseListActiveRequest() { UserId = claimsDTO.UserId };

                BaseResponse response = await service.GetPurchaseListActive(request);

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