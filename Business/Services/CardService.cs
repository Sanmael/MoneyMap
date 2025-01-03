using Business.Interfaces;
using Business.Mappers;
using Business.Models;
using Business.Requests.Card;
using Business.Response;
using Data.Repositories.Cards;
using Domain.Cards.Entities;
using Mapster;

namespace Business.Services
{
    public class CardService(CardRepositorie cardRepositorie) : ICardService
    {
        public async Task<BaseResponse> AddCardAsync(InsertCardRequest card)
        {
            Card? cardEntitie = CardMapper.MapInsertRequestToEntitie(card);

            if (cardEntitie == null)
                return new BaseResponse(new List<string>() { "Cartão nao encontrado" });

            if (!cardEntitie.IsValid())
                return new BaseResponse(new List<string>() { "Cartão está vencido" });

            await cardRepositorie.InsertAsync(cardEntitie);

            return new BaseResponse<CardModel>(cardEntitie.Adapt<CardModel>());
        }

        public async Task<BaseResponse> GetCardAsync(GetCardRequest card)
        {
            Card? cardEntitie = await cardRepositorie.GetCardAsync(card.UserId, card.CardId);

            if (cardEntitie == null)
                return new BaseResponse(new List<string>() { "cartão não encontrado!" });

            return new BaseResponse<CardModel>(CardMapper.MapperEntitieToModel(cardEntitie));
        }

        public async Task<BaseResponse> UpdateBalanceCardAsync(UpdateBalanceCardRequest updateCard)
        {
            Card? card = await cardRepositorie.GetCardAsync(updateCard.UserId, updateCard.CardId);

            if (card == null)
                return new BaseResponse(new List<string>() { "cartão não encontrado!" });

            card.Balance = updateCard.Balance;

            if (updateCard.Balance < 0)
                return new BaseResponse(new List<string>() { "Balance do cartão não pode ser negativo" });
            
            await cardRepositorie.UpdateAsync(card);

            return new BaseResponse<CardModel>(CardMapper.MapperEntitieToModel(card));
        }
    }
}