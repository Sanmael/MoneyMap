using Business.Interfaces;
using Business.Mappers;
using Business.Models;
using Business.Requests.Card;
using Data.Repositories.Cards;
using Domain.Cards.Entities;
using Mapster;

namespace Business.Services
{
    public class CardService(CardRepositorie cardRepositorie) : ICardService
    {
        public async Task<CardModel> AddCardAsync(InsertCardRequest card)
        {
            Card cardEntitie = CardMapper.MapInsertRequestToEntitie(card);

            if (!cardEntitie.IsValid())
            {
                throw new Exception();
            }

            await cardRepositorie.InsertAsync(cardEntitie);

            return cardEntitie.Adapt<CardModel>();
        }

        public async Task<CardModel> GetCardAsync(GetCardRequest card)
        {
            Card cardEntitie = await cardRepositorie.GetCardAsync(card.UserId, card.CardId) ?? throw new NullReferenceException("");

            return CardMapper.MapperEntitieToModel(cardEntitie);
        }

        public async Task<CardModel> UpdateBalanceCardAsync(UpdateBalanceCardRequest updateCard)
        {
            Card card = await cardRepositorie.GetCardAsync(updateCard.UserId, updateCard.CardId);
            
            card.Balance = updateCard.Balance;

            await cardRepositorie.UpdateAsync(card);

            return CardMapper.MapperEntitieToModel(card);
        }
    }
}