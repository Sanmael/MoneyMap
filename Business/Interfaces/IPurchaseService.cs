using Business.Requests;
using Business.Requests.Purchase;
using Business.Response;

namespace Business.Interfaces
{
    public interface IPurchaseService 
    {
        public Task<BaseResponse> AddPurchaseAsync(InsertPurchaseRequest purchaseInInstallmentsRequest);
        public Task<BaseResponse> GetPurchase(GetPurchaseRequest getPurchaseRequest);
        public Task<BaseResponse> GetPurchaseListActive(GetPurchaseListActiveRequest baseRequest);
    }
}