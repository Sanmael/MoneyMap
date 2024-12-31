using Domain.Base.Interfaces;
using Domain.Cards.Entities;

namespace Data.Repositories.Cards
{
    public class CardRepositorie(IBaseRepositorie<Card> baseRepositorie)
    {
        public async Task DeleteAsync(Card baseEntitie)
        {
            await baseRepositorie.Delete(baseEntitie);
        }

        public async Task InsertAsync(Card baseEntitie)
        {
            await baseRepositorie.InsertAsync(baseEntitie);
        }

        public async Task UpdateAsync(Card baseEntitie)
        {
            await baseRepositorie.Update(baseEntitie);
        }
    }
}