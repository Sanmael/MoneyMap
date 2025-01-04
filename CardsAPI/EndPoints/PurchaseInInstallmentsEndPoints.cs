using Business.Handlers.Filters;
using Business.Interfaces;
using Business.Requests.Card.PurchaseInInstallments;
using Business.Response;

namespace CardsAPI.EndPoints
{
    public static class PurchaseInInstallmentsEndPoints
    {
        public static void MapPurchaseInInstallmentsEndPoints(this WebApplication app)
        {
            app.MapPost("/AddPurchaseInInstallments", async (InsertPurchaseInInstallmentsRequest request, IPurchaseInInstallmentsService service) =>
            {
                BaseResponse response = await service.AddPurchaseInInstallmentsAsync(request);

                if (!response.Success)
                    return Results.BadRequest(response);

                return Results.Ok(response);

            }).
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();

            app.MapGet("/GetPurchaseInInstallments", async ([AsParameters] GetPurchaseInInstallmentsRequest request, IPurchaseInInstallmentsService service) =>
            {
                BaseResponse response = await service.GetPurchaseInInstallments(request);

                if (!response.Success)
                    return Results.BadRequest(response);

                return Results.Ok(response);

            }).
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();

            app.MapGet("/GetPurchaseInInstallmentsListActive", async ([AsParameters] GetPurchaseInInstallmentsListActiveRequest request, IPurchaseInInstallmentsService service) =>
            {
                BaseResponse response = await service.GetPurchaseInInstallmentsListActive(request);

                if (!response.Success)
                    return Results.BadRequest(response);

                return Results.Ok(response);

            }).
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();
        }
    }
}
