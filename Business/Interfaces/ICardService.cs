using Business.Models;

namespace Business.Interfaces
{
    public interface ICardService
    {
        public Task AddCardAsync(CardModel card);
    }
}