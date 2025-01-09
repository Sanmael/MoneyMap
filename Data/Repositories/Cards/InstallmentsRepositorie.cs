using Domain.Base.Interfaces.Repositories;
using Domain.Cards.Entities;

namespace Data.Repositories.Cards
{
    public class InstallmentsRepositorie(IBaseRepositorie<Installments> baseRepositorie) : IInstallmentsRepositorie
    {
        public async Task InsertPurchaseInInstallmentsAsync(Installments baseEntitie)
        {
            await baseRepositorie.InsertAsync(baseEntitie);
        }
    }
}