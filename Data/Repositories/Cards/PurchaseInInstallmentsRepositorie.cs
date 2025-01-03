using Data.Context;
using Domain.Base.Interfaces;
using Domain.Cards.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Cards
{
    public class PurchaseInInstallmentsRepositorie(IBaseRepositorie<PurchaseInInstallments> baseRepositorie,
        EntityFramework entityFramework)
    {
        public async Task DeletePurchaseInInstallmentsAsync(PurchaseInInstallments baseEntitie)
        {
            await baseRepositorie.Delete(baseEntitie);
        }

        public async Task InsertPurchaseInInstallmentsAsync(PurchaseInInstallments baseEntitie)
        {
            await baseRepositorie.InsertAsync(baseEntitie);
        }

        public async Task UpdatePurchaseInInstallmentsAsync(PurchaseInInstallments baseEntitie)
        {
            await baseRepositorie.Update(baseEntitie);
        }

        public async Task<PurchaseInInstallments?> GetPurchaseInInstallmentsAsync(long purchaseInInstallmentsId)
        {
            PurchaseInInstallments? purchaseInInstallments = await entityFramework.PurchaseInInstallments.Include(x => x.Installments).FirstAsync(x=> x.Id == purchaseInInstallmentsId);

            return purchaseInInstallments;
        }

        public async Task<List<PurchaseInInstallments>>? GetPurchaseInInstallmentsActiveAsync(long cardId)
        {
            List<PurchaseInInstallments>? query = await entityFramework.PurchaseInInstallments.Include(x => x.Installments).Where(x=> x.CardId == cardId && !x.Paid).ToListAsync();

            return query;
        }
    }
}