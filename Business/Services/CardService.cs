using Business.Interfaces;
using Business.Mappers;
using Business.Models;
using Data.Repositories.Cards;
using Domain.Cards.Entities;

namespace Business.Services
{
    public class CardService(CardRepositorie cardRepositorie) : ICardService
    {
        public async Task AddCardAsync(CardModel card)
        {
            Card cardEntitie = CardMapper.MapperModelToEntitie(card);

            if (!cardEntitie.IsValid())
            {
                throw new Exception();
            }

            await cardRepositorie.InsertAsync(cardEntitie);
        }
    }
}