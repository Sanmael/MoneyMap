using Business.Extensions;
using Business.Models;
using Business.Requests.Card;
using Domain.Base.Entities;
using Domain.Cards.Entities;
using Mapster;

namespace Business.Mappers
{
    public static class CardMapper
    {
        public static Card MapInsertRequestToEntitie(InsertCardRequest card)
        {
            if (card == null)
                throw new ArgumentNullException(nameof(card));

            return new Card()
            {
                UserId = card.UserId,
                DueDate = card.DueDate,
                Name = card.Name,
                Description = card.Description,
                Limit = card.Limit,
                Balance = card.Balance,
                CategorieId = card.CategorieId 
            };
        }

        public static CardModel MapperEntitieToModel(Card card)
        {
            if (card == null)
                throw new ArgumentNullException(nameof(card));

            return card.MapperEntitieToModel();
        }
    }
}