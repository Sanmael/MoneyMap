using Business.Extensions;
using Business.Interfaces;
using Business.Models;
using Business.Requests.Card;
using Business.Requests.Card.PurchaseInInstallments;
using Data.Repositories.Cards;
using Domain.Cards.Entities;

namespace Business.Services
{
    public class PurchaseInInstallmentsService(ICardService cardService, PurchaseInInstallmentsRepositorie purchaseInInstallmentsRepositorie) : IPurchaseInInstallmentsService
    {
        public async Task<PurchaseInInstallmentsModel> AddPurchaseInInstallmentsAsync(InsertPurchaseInInstallmentsRequest purchaseInInstallmentsRequest)
        {
            CardModel card = await cardService.GetCardAsync(new GetCardRequest()
            {
                CardId = purchaseInInstallmentsRequest.CardId,
                UserId = purchaseInInstallmentsRequest.UserId
            });

            if (card == null)
                throw new NotImplementedException();

            PurchaseInInstallments purchaseInInstallments = purchaseInInstallmentsRequest.MapperInsertRequestToEntitie();

            decimal newBalance = card.Balance - purchaseInInstallments.TotalPrice;

            await cardService.UpdateBalanceCardAsync(new UpdateBalanceCardRequest() { Balance = newBalance, CardId = card.Id, UserId = card.UserId });

            await purchaseInInstallmentsRepositorie.InsertPurchaseInInstallmentsAsync(purchaseInInstallments);

            return purchaseInInstallments.MapperEntitieToModel();
        }
    }
}
