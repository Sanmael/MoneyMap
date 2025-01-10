using Business.Models;
using Business.Requests.Card;
using Business.Requests.Card.PurchaseInInstallments;
using Domain.Base.Entities;
using Domain.Cards.Entities;

namespace Business.Extensions
{
    public static class CardExtension
    {
        public static CardModel MapperEntitieToModel(this Card card)
        {
            CardModel cardModel = new CardModel
            {
                Id = card.Id,
                Name = card.Name,
                UserId = card.UserId,
                CategorieId = card.CategorieId,
                DueDate = card.DueDate,
                Balance = card.Balance,
                Description = card.Description,
                Limit = card.Limit
            };

            if (card.User != null)
            {
                cardModel.User = new UserModel()
                {
                    Id = card.User.Id,
                    Balance = card.User.Balance,
                    Name = card.User.Name,
                    Salary = card.User.Salary,
                    Email = card.User.Email
                };
            }

            if (card.Categorie != null)
            {
                cardModel.Categorie = new PurchaseCategorieModel()
                {
                    Name = card.Categorie!.Name
                };
            }

            return cardModel;
        }

        public static Card MapInsertRequestToEntitie(this InsertCardRequest card)
        {
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
    }

    public static class PurchaseInInstallmentsExtension
    {
        public static PurchaseInInstallments MapperInsertRequestToEntitie(this InsertPurchaseInInstallmentsRequest purchaseInInstallments)
        {
            return new PurchaseInInstallments
            {
                DateOfPurchase = purchaseInInstallments.DateOfPurchase,
                TotalPrice = purchaseInInstallments.GetTotalValue(),
                UserId = purchaseInInstallments.UserId,
                CardId = purchaseInInstallments.CardId,
                Name = purchaseInInstallments.Name,
                Description = purchaseInInstallments.Description,
                CategorieId = purchaseInInstallments.CategorieId
            };
        }

        private static List<InstallmentsModel> MapInstallments(List<Installments> installments)
        {
            return installments.Select(installment => new InstallmentsModel
            {
                Id = installment.Id,
                Value = installment.Value,
                PurchaseInInstallmentsId = installment.PurchaseInInstallmentsId,
                InstallmentNumber = installment.InstallmentNumber,
                ReferringMonth = installment.ReferringMonth,
                Paid = installment.Paid
            }).ToList();
        }

        public static PurchaseInInstallmentsModel MapperEntitieToModel(this PurchaseInInstallments entitie, List<Installments>? installments = null)
        {
            PurchaseInInstallmentsModel purchaseInInstallmentsModel = new PurchaseInInstallmentsModel
            {
                Id = entitie.Id,
                Paid = entitie.Paid,
                CardId = entitie.CardId,
                Name = entitie.Name,
                Description = entitie.Description,
                TotalPrice = entitie.TotalPrice,
                CategorieId = entitie.CategorieId,
                InstallmentsModels = new List<InstallmentsModel>()
            };

            if (entitie.Installments != null)
            {
                purchaseInInstallmentsModel.InstallmentsModels.AddRange(MapInstallments(entitie.Installments));
            }

            if (installments != null)
            {
                var newInstallments = MapInstallments(installments)
                    .Where(x => !purchaseInInstallmentsModel.InstallmentsModels.Any(existing => existing.Id == x.Id))
                    .ToList();

                purchaseInInstallmentsModel.InstallmentsModels.AddRange(newInstallments);
            }

            purchaseInInstallmentsModel.NumberOfInstallments = purchaseInInstallmentsModel.InstallmentsModels.Count;

            return purchaseInInstallmentsModel;
        }
    }
}