using Domain.Base.Interfaces.Repositories;
using Domain.Cards.Entities;

namespace Data.Cache.Repositories
{
    public class CardCache(ICardRepositorie cardRepositorie, RedisCacheRepositorie redisCacheRepositorie) : ICardRepositorie
    {
        public async Task DeleteCardAsync(Card card)
        {
            await cardRepositorie.DeleteCardAsync(card);
            await RemoveAllCachesAsync(card);
        }

        public async Task<List<Card>>? GetAllCardsAsync(Guid userId)
        {
            List<Card>? cache = await redisCacheRepositorie.GetCache<List<Card>>(userId.ToString());

            if (cache != null)
                return cache;

            List<Card>? cards = await cardRepositorie.GetAllCardsAsync(userId);

            if (cards != null)
                await redisCacheRepositorie.SaveCache(cards, userId.ToString());

            return cards;
        }

        public async Task<Card?> GetCardAsync(Guid userId, Guid id)
        {
            Card? cache = await redisCacheRepositorie.GetCache<Card>(id.ToString());

            if (cache != null)
                return cache;

            Card? card = await cardRepositorie.GetCardAsync(userId, id);

            if (card != null)
                await redisCacheRepositorie.SaveCache(card, id.ToString());

            return card;
        }

        public async Task InsertCardAsync(Card card)
        {
            await cardRepositorie.InsertCardAsync(card);
            await RemoveAllCachesAsync(card);
            await redisCacheRepositorie.SaveCache(card, card.Id.ToString(), nameof(GetCardAsync));
        }

        public async Task UpdateCardAsync(Card card)
        {
            await cardRepositorie.UpdateCardAsync(card);
            await RemoveAllCachesAsync(card);
            await redisCacheRepositorie.SaveCache(card, card.Id.ToString());
        }

        private async Task RemoveAllCachesAsync(Card card)
        {
            await redisCacheRepositorie.RemoveCache(nameof(GetAllCardsAsync) + card.UserId);
            await redisCacheRepositorie.RemoveCache(nameof(GetCardAsync) + card.Id);
        }
    }
}