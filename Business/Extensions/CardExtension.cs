using Business.Models;
using Business.Requests.Card.PurchaseInInstallments;
using Domain.Cards.Entities;

namespace Business.Extensions
{
    public static class CardExtension
    {
        public static CardModel MapperEntitieToModel(this Card card)
        {
            return new CardModel
            {
                Id = card.Id,
                Name = card.Name,
                User = new UserModel()
                {
                    Id = card.User.Id,
                    Balance = card.User.Balance,
                    Name = card.User.Name,
                    Salary = card.User.Salary,
                    Email = card.User.Email
                },
                UserId = card.UserId,
                CategorieId = card.CategorieId,
                Categorie = new PurchaseCategorieModel()
                {
                    Name = card.Categorie.Name
                },
                DueDate = card.DueDate,
                Balance = card.Balance,
                Description = card.Description,
                Limit = card.Limit
            };
        }
    }
    public static class PurchaseInInstallmentsExtension
    {
        public static PurchaseInInstallments MapperInsertRequestToEntitie(this InsertPurchaseInInstallmentsRequest purchaseInInstallments)
        {
            return new PurchaseInInstallments
            {
                UserId = purchaseInInstallments.UserId,
                CardId = purchaseInInstallments.CardId,
                NumberOfInstallments = purchaseInInstallments.NumberOfInstallments,
                InstallmentValue = purchaseInInstallments.InstallmentValue,
                Name = purchaseInInstallments.Name,
                Description = purchaseInInstallments.Description,
                CategorieId = purchaseInInstallments.CategorieId
            };
        }

        public static PurchaseInInstallmentsModel MapperEntitieToModel(this PurchaseInInstallments entitie)
        {
            return new PurchaseInInstallmentsModel
            {
                CardId = entitie.CardId,
                NumberOfInstallments = entitie.NumberOfInstallments,
                InstallmentValue = entitie.InstallmentValue,
                Name = entitie.Name,
                Description = entitie.Description,
                TotalPrice = entitie.TotalPrice,
                CategorieId = entitie.CategorieId
            };
        }
    }
}