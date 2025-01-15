using Business.Requests;
using Business.Requests.Card.PurchaseInInstallments;
using Business.Response;

namespace Business.Interfaces
{
    public interface IPurchaseInInstallmentsService
    {
        public Task<BaseResponse> AddPurchaseInInstallmentsAsync(InsertPurchaseInInstallmentsRequest purchaseInInstallmentsRequest);
        public Task<BaseResponse> GetPurchaseInInstallments(GetPurchaseInInstallmentsRequest getPurchaseInInstallmentsRequest);
        public Task<BaseResponse> GetPurchaseInInstallmentsListActive(GetPurchaseInInstallmentsListActiveRequest getPurchaseInInstallmentsListActiveRequest);
        public Task<BaseResponse> GetAllPurchaseInInstallmentsListActive(GetAllPurchaseInInstallmentsListActiveRequest baseRequest);
        public Task<BaseResponse> GetAllPurchaseInInstallmentsListActiveByDate(GetAllPurchaseInInstallmentsListActiveByDateRequest getAllPurchaseInInstallmentsListActiveByDateRequest);
        public Task<BaseResponse> GetActiveInstallmentsByMonthAsync(GetActiveInstallmentsByMonthAsyncRequest getActiveInstallmentsByMonthAsyncRequest);
    }
}
