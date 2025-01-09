using Data.Context;
using Domain.Base.Interfaces.Repositories;
using Domain.Cards.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Cards
{
    public class CardRepositorie(IBaseRepositorie<Card> baseRepositorie, EntityFramework entityFramework) : ICardRepositorie
    {
        public async Task DeleteCardAsync(Card baseEntitie)
        {
            await baseRepositorie.Delete(baseEntitie);
        }

        public async Task InsertCardAsync(Card baseEntitie)
        {
            await baseRepositorie.InsertAsync(baseEntitie);
        }

        public async Task UpdateCardAsync(Card baseEntitie)
        {
            await baseRepositorie.Update(baseEntitie);
        }

        public async Task<Card?> GetCardAsync(Guid userId, Guid id)
        {
            Card? card = await entityFramework.Card.Include(x=> x.User).Include(x=> x.Categorie).FirstOrDefaultAsync(x=> x.UserId == userId && 
            x.Id == id);

            return card;
        }

        public async Task<List<Card>?> GetAllCardsAsync(Guid userId)
        {
            return await entityFramework.Card.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}