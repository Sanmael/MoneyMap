using Business.Models;
using Business.Requests.Card.PurchaseInInstallments;

namespace Business.Interfaces
{
    public interface IPurchaseInInstallmentsService
    {
        public Task<PurchaseInInstallmentsModel> AddPurchaseInInstallmentsAsync(InsertPurchaseInInstallmentsRequest purchaseInInstallmentsRequest);
    }
}
