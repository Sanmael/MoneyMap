using Business.Handlers.Filters;
using Business.Interfaces;
using Business.Requests.Purchase;
using Business.Response;

namespace PurchaseAPI.EndPoints
{
    public static class PurchaseEndPoints
    {
        public static void MapPurchaseEndPoints(this WebApplication app)
        {
            app.MapPost("/AddPurchase", async (InsertPurchaseRequest request, IPurchaseService service) =>
            {
                BaseResponse response = await service.AddPurchaseAsync(request);

                if (!response.Success)
                    return Results.BadRequest(response);

                response.Location = $"/GetPurchase?userId={request.UserId}&purchaseId={response.GetData<Guid>()}";

                return Results.Created(response.Location, response);
            }).
            RequireAuthorization().
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();

            app.MapGet("/GetPurchase", async ([AsParameters] GetPurchaseRequest request, IPurchaseService service) =>
            {
                BaseResponse response = await service.GetPurchase(request);

                if (!response.Success)
                    return Results.NotFound(response);

                return Results.Ok(response);
            }).
            RequireAuthorization().
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();

            app.MapGet("/GetPurchasesActive", async ([AsParameters] GetPurchaseListActiveRequest request, IPurchaseService service) =>
            {
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