using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IPurchaseRepositorie
    {
        public Task InsertPurchaseAsync(Purchase purchase);
        public Task UpdatePurchaseAsync(Purchase purchase);
        public Task<Purchase?> GetPurchaseAsync(Guid userId,Guid purchaseId);
        public Task<List<Purchase>?> GetPurchaseActiveAsync(Guid userId);
        public Task<List<Purchase>> GetPurchaseActiveByDate(Guid userId, DateTime? firstDate, DateTime? lastDate);
    }
}