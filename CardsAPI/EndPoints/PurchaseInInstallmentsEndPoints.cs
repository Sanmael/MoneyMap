using Business.Handlers.Filters;
using Business.Interfaces;
using Business.Requests;
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

                response.Location = $"/GetPurchaseInInstallments?userId={request.UserId}&purchaseInInstallmentsId={response.GetData<Guid>()}";

                return Results.Created(response.Location, response);
            }).
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();

            app.MapGet("/GetPurchaseInInstallments", async ([AsParameters] GetPurchaseInInstallmentsRequest request, IPurchaseInInstallmentsService service) =>
            {
                BaseResponse response = await service.GetPurchaseInInstallments(request);

                if (!response.Success)
                    return Results.NotFound(response);

                return Results.Ok(response);
            }).
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();

            app.MapGet("/GetPurchaseInInstallmentsListActiveByCardId", async ([AsParameters] GetPurchaseInInstallmentsListActiveRequest request, IPurchaseInInstallmentsService service) =>
            {
                BaseResponse response = await service.GetPurchaseInInstallmentsListActive(request);

                if (!response.Success)
                    return Results.NotFound(response);

                return Results.Ok(response);
                }).
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();

            app.MapGet("/GetAllPurchaseInInstallmentsListActive", async ([AsParameters] BaseRequest request, IPurchaseInInstallmentsService service) =>
            {
                BaseResponse response = await service.GetAllPurchaseInInstallmentsListActive(request);

                if (!response.Success)
                    return Results.NotFound(response);

                return Results.Ok(response);
            }).
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();

            app.MapGet("/GetAllPurchaseInInstallmentsListActiveByDate", async ([AsParameters] GetAllPurchaseInInstallmentsListActiveByDateRequest request, IPurchaseInInstallmentsService service) =>
            {
                BaseResponse response = await service.GetAllPurchaseInInstallmentsListActiveByDate(request);

                if (!response.Success)
                    return Results.NotFound(response);

                return Results.Ok(response);
            }).
            AddEndpointFilter<LoggerFilter>().
            AddEndpointFilter<ValidationFilter>();

            app.MapGet("/GetActiveInstallmentsByMonthAsync", async ([AsParameters] GetActiveInstallmentsByMonthAsyncRequest request, IPurchaseInInstallmentsService service) =>
            {
                BaseResponse response = await service.GetActiveInstallmentsByMonthAsync(request);

                if (!response.Success)
                    return Results.NotFound(response);

                return Results.Ok(response);
            }).
           AddEndpointFilter<LoggerFilter>().
           AddEndpointFilter<ValidationFilter>();
        }
    }
}