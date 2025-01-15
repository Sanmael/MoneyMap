using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Data.Cache.Repositories
{
    public class PurchaseCache(IPurchaseRepositorie purchaseRepositorie, RedisCacheRepositorie redisCacheRepositorie) : IPurchaseRepositorie
    {
        public async Task<List<Purchase>?> GetPurchaseActiveAsync(Guid userId)
        {
            List<Purchase>? purchaseList = await redisCacheRepositorie.GetCache<List<Purchase>>(userId.ToString());

            if (purchaseList != null)
                return purchaseList;

            purchaseList = await purchaseRepositorie.GetPurchaseActiveAsync(userId);

            if (purchaseList != null)
                await redisCacheRepositorie.SaveCache(purchaseList, userId.ToString());

            return purchaseList;
        }

        public Task<List<Purchase>> GetPurchaseActiveByDate(Guid userId, DateTime? firstDate, DateTime? lastDate)
        {
            return purchaseRepositorie.GetPurchaseActiveByDate(userId, firstDate, lastDate);
        }

        public async Task<Purchase?> GetPurchaseAsync(Guid purchaseId)
        {
            Purchase? purchase = await redisCacheRepositorie.GetCache<Purchase>(purchaseId.ToString());

            if (purchase != null)
                return purchase;

            purchase = await purchaseRepositorie.GetPurchaseAsync(purchaseId);

            if (purchase != null)
                await redisCacheRepositorie.SaveCache(purchase, purchaseId.ToString());

            return purchase;
        }

        public async Task InsertPurchaseAsync(Purchase purchase)
        {
            await purchaseRepositorie.InsertPurchaseAsync(purchase);
            await RemoveAllCachesAsync(purchase);
            await redisCacheRepositorie.SaveCache(purchase, purchase.Id.ToString(), nameof(GetPurchaseAsync));
        }

        public async Task UpdatePurchaseAsync(Purchase purchase)
        {
            await purchaseRepositorie.UpdatePurchaseAsync(purchase);
            await RemoveAllCachesAsync(purchase);
            await redisCacheRepositorie.SaveCache(purchase, purchase.Id.ToString(), nameof(GetPurchaseAsync));
        }

        private async Task RemoveAllCachesAsync(Purchase purchase)
        {
            await redisCacheRepositorie.RemoveCache(nameof(GetPurchaseAsync) + purchase.Id);
            await redisCacheRepositorie.RemoveCache(nameof(GetPurchaseActiveAsync) + purchase.UserId);
        }
    }
}
