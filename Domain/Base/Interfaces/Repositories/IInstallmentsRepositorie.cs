using Domain.Cards.Entities;

namespace Domain.Base.Interfaces.Repositories
{
    public interface IInstallmentsRepositorie
    {
        public Task InsertPurchaseInInstallmentsAsync(Installments baseEntitie);        
    }
}
