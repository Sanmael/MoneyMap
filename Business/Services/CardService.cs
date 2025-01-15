using Business.Extensions;
using Business.Interfaces;
using Business.Models;
using Business.Requests.Card;
using Business.Response;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Business.Services
{
    public class CardService(ICardRepositorie cardRepositorie) : ICardService
    {
        public async Task<BaseResponse> AddCardAsync(InsertCardRequest card)
        {
            Card? cardEntitie = card.MapInsertRequestToEntitie();

            if (cardEntitie == null)
               return new BaseResponse("Cartão nao encontrado");

            if (!cardEntitie.IsValid())
                return new BaseResponse("Cartão está vencido");

            await cardRepositorie.InsertCardAsync(cardEntitie);

            return new BaseResponse<Guid>(cardEntitie.Id, "cartão criado com sucesso");
        }

        public async Task<BaseResponse> GetAllCardAsync(GetAllCardsRequest card)
        {
            List<Card>? cards = await cardRepositorie.GetAllCardsAsync(card.UserId);

            if(cards == null || cards.Count == 0)
                return new BaseResponse("Não há cartões registrados");

            return new BaseResponse<List<CardModel>>(cards.Select(x => x.MapperEntitieToModel()).ToList());
        }

        public async Task<BaseResponse> GetCardAsync(GetCardRequest card)
        {
            Card? cardEntitie = await cardRepositorie.GetCardAsync(card.UserId, card.CardId);

            if (cardEntitie == null)
                return new BaseResponse("cartão não encontrado!");

            return new BaseResponse<CardModel>(cardEntitie.MapperEntitieToModel());
        }

        public async Task<BaseResponse> UpdateBalanceCardAsync(UpdateBalanceCardRequest updateCard)
        {
            Card? card = await cardRepositorie.GetCardAsync(updateCard.UserId, updateCard.CardId);

            if (card == null)
                return new BaseResponse("cartão não encontrado!");

            card.Balance = updateCard.Balance;

            if (updateCard.Balance < 0)
                return new BaseResponse("Balance do cartão não pode ser negativo");
            
            await cardRepositorie.UpdateCardAsync(card);

            return new BaseResponse<CardModel>(card.MapperEntitieToModel());
        }
    }
}