﻿using Domain.Cards.Entities;

namespace Domain.Base.Interfaces.Repositories
{
    public interface IPurchaseInInstallmentsRepositorie
    {
        public Task DeletePurchaseInInstallmentsAsync(PurchaseInInstallments baseEntitie);
        public Task InsertPurchaseInInstallmentsAsync(PurchaseInInstallments baseEntitie);
        public Task UpdatePurchaseInInstallmentsAsync(PurchaseInInstallments baseEntitie);
        public Task<PurchaseInInstallments?> GetPurchaseInInstallmentsAsync(Guid purchaseInInstallmentsId);
        public Task<List<PurchaseInInstallments>>? GetPurchaseInInstallmentsActiveByCardIdAsync(Guid cardId);
        public Task<List<PurchaseInInstallments>>? GetAllPurchaseInInstallmentsActiveAsync(Guid userId);
    }
}