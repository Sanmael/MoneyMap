using Business.Models;
using Business.Requests.Purchase;
using Domain.Entities;

namespace Business.Extensions
{
    public static class PurchaseExtension
    {
        public static Purchase MapperInsertRequestToEntitie(this InsertPurchaseRequest insertPurchaseRequest)
        {
            return new Purchase
            {
                DateOfPurchase = insertPurchaseRequest.DateOfPurchase,                
                UserId = insertPurchaseRequest.UserId,                
                Name = insertPurchaseRequest.Name,
                Description = insertPurchaseRequest.Description,
                CategorieId = insertPurchaseRequest.CategorieId,
                TotalPrice = insertPurchaseRequest.Value
            };
        }

        public static PurchaseModel MapperEntitieToModel(this Purchase entitie)
        {
            PurchaseModel purchaseModel = new PurchaseModel
            {
                UserId = entitie.UserId,
                Id = entitie.Id,
                Paid = entitie.Paid,                
                Name = entitie.Name,
                Description = entitie.Description,
                TotalPrice = entitie.TotalPrice,
                CategorieId = entitie.CategorieId
            };

            if (entitie.User != null)
            {
                purchaseModel.User = new UserModel()
                {
                    Id = entitie.User.Id,
                    Balance = entitie.User.Balance,
                    Name = entitie.User.Name,
                    Salary = entitie.User.Salary,
                    Email = entitie.User.Email
                };
            }

            if (entitie.Categorie != null)
            {
                purchaseModel.Categorie = new PurchaseCategorieModel()
                {
                    Name = entitie.Categorie!.Name
                };
            }
            return purchaseModel;
        }
    }
}