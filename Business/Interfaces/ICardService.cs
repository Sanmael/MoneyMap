using Business.Models;
using Business.Requests.Card;

namespace Business.Interfaces
{
    public interface ICardService
    {
        public Task<CardModel> AddCardAsync(InsertCardRequest card);
        public Task<CardModel> GetCardAsync(GetCardRequest card);
        public Task<CardModel> UpdateBalanceCardAsync(UpdateBalanceCardRequest card);
    }
}