using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Data.Cache.Repositories
{
    public class PurchaseInInstallmentsCache(IPurchaseInInstallmentsRepositorie purchaseInInstallmentsRepositorie, RedisCacheRepositorie redisCacheRepositorie)
        : IPurchaseInInstallmentsRepositorie
    {
        public async Task DeletePurchaseInInstallmentsAsync(PurchaseInInstallments baseEntitie)
        {
            await purchaseInInstallmentsRepositorie.DeletePurchaseInInstallmentsAsync(baseEntitie);
            await RemoveAllCachesAsync(baseEntitie);
        }

        public async Task InsertPurchaseInInstallmentsAsync(PurchaseInInstallments baseEntitie)
        {
            await purchaseInInstallmentsRepositorie.InsertPurchaseInInstallmentsAsync(baseEntitie);
            await RemoveAllCachesAsync(baseEntitie);
            await redisCacheRepositorie.SaveCache(baseEntitie, baseEntitie.Id.ToString(), nameof(GetPurchaseInInstallmentsAsync));
        }

        public async Task UpdatePurchaseInInstallmentsAsync(PurchaseInInstallments baseEntitie)
        {
            await purchaseInInstallmentsRepositorie.UpdatePurchaseInInstallmentsAsync(baseEntitie);
            await RemoveAllCachesAsync(baseEntitie);
            await redisCacheRepositorie.SaveCache(baseEntitie, baseEntitie.Id.ToString());
        }

        public async Task<PurchaseInInstallments?> GetPurchaseInInstallmentsAsync(Guid purchaseInInstallmentsId, Guid userId)
        {
            PurchaseInInstallments? cached = await redisCacheRepositorie.GetCache<PurchaseInInstallments>(purchaseInInstallmentsId.ToString());

            if (cached != null)
                return cached;

            PurchaseInInstallments? purchaseInInstallments = await purchaseInInstallmentsRepositorie.GetPurchaseInInstallmentsAsync(purchaseInInstallmentsId, userId);

            if (purchaseInInstallments != null)
                await redisCacheRepositorie.SaveCache(purchaseInInstallments, purchaseInInstallments.Id.ToString());

            return purchaseInInstallments;
        }

        public async Task<List<PurchaseInInstallments>>? GetPurchaseInInstallmentsActiveByCardIdAsync(Guid cardId)
        {
            List<PurchaseInInstallments>? cached = await redisCacheRepositorie.GetCache<List<PurchaseInInstallments>>(cardId.ToString());

            if (cached != null && cached.Count > 0)
                return cached;

            List<PurchaseInInstallments>? purchaseInInstallments = await purchaseInInstallmentsRepositorie.GetPurchaseInInstallmentsActiveByCardIdAsync(cardId);

            if (purchaseInInstallments != null)
                await redisCacheRepositorie.SaveCache(purchaseInInstallments, cardId.ToString());

            return purchaseInInstallments;
        }

        public async Task<List<PurchaseInInstallments>>? GetAllPurchaseInInstallmentsActiveAsync(Guid userId)
        {
            List<PurchaseInInstallments>? cached = await redisCacheRepositorie.GetCache<List<PurchaseInInstallments>>(userId.ToString());

            if (cached != null)
                return cached;

            List<PurchaseInInstallments>? purchaseInInstallments = await purchaseInInstallmentsRepositorie.GetAllPurchaseInInstallmentsActiveAsync(userId);

            if (purchaseInInstallments != null)
                await redisCacheRepositorie.SaveCache(purchaseInInstallments, userId.ToString());

            return purchaseInInstallments;
        }

        private async Task RemoveAllCachesAsync(PurchaseInInstallments purchaseInInstallments)
        {
            await redisCacheRepositorie.RemoveCache(nameof(GetPurchaseInInstallmentsAsync) + purchaseInInstallments.Id);
            await redisCacheRepositorie.RemoveCache(nameof(GetPurchaseInInstallmentsActiveByCardIdAsync) + purchaseInInstallments.CardId);
            await redisCacheRepositorie.RemoveCache(nameof(GetAllPurchaseInInstallmentsActiveAsync) + purchaseInInstallments.UserId);
        }

        public Task<List<PurchaseInInstallments>> GetAllPurchaseInInstallmentsByDateAsync(Guid userId, DateTime? firstDate, DateTime? lastDate)
        {
            return purchaseInInstallmentsRepositorie.GetAllPurchaseInInstallmentsByDateAsync(userId, firstDate, lastDate);
        }
    }
}