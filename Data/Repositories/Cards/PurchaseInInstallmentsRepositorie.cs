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

        public async Task<PurchaseInInstallments?> GetPurchaseInInstallmentsAsync(long cardId)
        {            
            PurchaseInInstallments? purchaseInInstallments = await entityFramework.PurchaseInInstallments.FirstOrDefaultAsync(x=> x.CardId == cardId);
            
            return purchaseInInstallments;
        }
    }
}