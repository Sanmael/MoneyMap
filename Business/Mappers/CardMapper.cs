using Business.Models;
using Domain.Cards.Entities;
using Mapster;

namespace Business.Mappers
{
    public static class CardMapper
    {
        public static Card MapperModelToEntitie(CardModel card)
        {
            if (card == null)
                throw new ArgumentNullException(nameof(card));

            return card.Adapt<Card>();
        }

        public static CardModel MapperEntitieToModel(Card card)
        {
            if (card == null)
                throw new ArgumentNullException(nameof(card));

            return card.Adapt<CardModel>();
        }
    }
}