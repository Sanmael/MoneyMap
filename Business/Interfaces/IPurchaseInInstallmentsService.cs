using Business.Models;
using Business.Requests.Card.PurchaseInInstallments;
using Business.Response;

namespace Business.Interfaces
{
    public interface IPurchaseInInstallmentsService
    {
        public Task<BaseResponse> AddPurchaseInInstallmentsAsync(InsertPurchaseInInstallmentsRequest purchaseInInstallmentsRequest);
        public Task<BaseResponse> GetPurchaseInInstallments(GetPurchaseInInstallmentsRequest getPurchaseInInstallmentsRequest);
        public Task<BaseResponse> GetPurchaseInInstallmentsListActive(long cardId,long userId);
    }
}
