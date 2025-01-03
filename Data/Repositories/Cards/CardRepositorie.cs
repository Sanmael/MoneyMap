using Data.Context;
using Domain.Base.Interfaces;
using Domain.Cards.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Cards
{
    public class CardRepositorie(IBaseRepositorie<Card> baseRepositorie, EntityFramework entityFramework)
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

        public async Task<Card?> GetCardAsync(long userId,long id)
        {
            Card? card = await entityFramework.Card.Include(x=> x.User).Include(x=> x.Categorie).FirstOrDefaultAsync(x=> x.UserId == userId && 
            x.Id == id);

            return card;
        }
    }
}