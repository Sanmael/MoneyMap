using Domain.Base.Entities;

namespace Domain.Base.Interfaces.Repositories
{
    public interface IPurchaseRepositorie
    {
        public Task InsertPurchaseAsync(Purchase purchase);
        public Task UpdatePurchaseAsync(Purchase purchase);
        public Task<Purchase?> GetPurchaseAsync(Guid purchaseId);
        public Task<List<Purchase>?> GetPurchaseActiveAsync(Guid userId);
    }
}
