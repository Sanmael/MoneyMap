using Data.Context;
using Domain.Base.Interfaces;
using Domain.Cards.Entities;

namespace Data.Repositories.Cards
{
    public class InstallmentsRepositorie(IBaseRepositorie<Installments> baseRepositorie,
        EntityFramework entityFramework)
    {
        public async Task InsertPurchaseInInstallmentsAsync(Installments baseEntitie)
        {
            await baseRepositorie.InsertAsync(baseEntitie);
        }
    }
}