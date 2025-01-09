using Domain.Cards.Entities;

namespace Domain.Base.Interfaces.Repositories
{
    public interface ICardRepositorie
    {
        public Task InsertCardAsync(Card card);
        public Task DeleteCardAsync(Card card);
        public Task UpdateCardAsync(Card card);
        public Task<Card?> GetCardAsync(Guid userId, Guid id);
        public Task<List<Card>>? GetAllCardsAsync(Guid userId);
    }
}