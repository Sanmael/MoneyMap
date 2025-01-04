using Business.Extensions;
using Business.Interfaces;
using Business.Models;
using Business.Requests.Card;
using Business.Requests.Card.PurchaseInInstallments;
using Business.Response;
using Data.Repositories.Cards;
using Domain.Cards.Entities;

namespace Business.Services
{
    public class PurchaseInInstallmentsService(ICardService cardService, PurchaseInInstallmentsRepositorie purchaseInInstallmentsRepositorie,
        InstallmentsRepositorie installmentsRepositorie) : IPurchaseInInstallmentsService
    {
        public async Task<BaseResponse> AddPurchaseInInstallmentsAsync(InsertPurchaseInInstallmentsRequest purchaseInInstallmentsRequest)
        {
            BaseResponse response = await cardService.GetCardAsync(new GetCardRequest()
            {
                CardId = purchaseInInstallmentsRequest.CardId,
                UserId = purchaseInInstallmentsRequest.UserId
            });

            if (!response.Success)
                return new BaseResponse(new List<string>() { "cartão não encontrado ou não pertence ao usuario" });

            PurchaseInInstallments purchaseInInstallments = purchaseInInstallmentsRequest.MapperInsertRequestToEntitie();

            CardModel card = response.GetEntitie<CardModel>();

            decimal newBalance = card.Balance - purchaseInInstallments.TotalPrice;

            await cardService.UpdateBalanceCardAsync(new UpdateBalanceCardRequest() { Balance = newBalance, CardId = card.Id, UserId = card.UserId });

            await purchaseInInstallmentsRepositorie.InsertPurchaseInInstallmentsAsync(purchaseInInstallments);

            if (purchaseInInstallmentsRequest.CustomInstallments != null && purchaseInInstallmentsRequest.CustomInstallments.Any())
            {
                decimal totalCustomInstallments = purchaseInInstallmentsRequest.CustomInstallments.Sum(x => x.Value);

                if (totalCustomInstallments != purchaseInInstallments.TotalPrice)
                    return new BaseResponse(new List<string>() { "O total das parcelas personalizadas não coincide com o valor total da compra." });

                foreach (var customInstallment in purchaseInInstallmentsRequest.CustomInstallments)
                {
                    await InsertCustomizedInstallmentAsync(purchaseInInstallments, customInstallment);
                }
            }

            else
            {
                for (int i = 0; i < purchaseInInstallmentsRequest.NumberOfInstallments; i++)
                {
                    await InsertInstallmentsAsync(purchaseInInstallments, i, purchaseInInstallmentsRequest.InstallmentValue);
                }
            }

            return new BaseResponse<PurchaseInInstallmentsModel>(purchaseInInstallments.MapperEntitieToModel());
        }

        public async Task<BaseResponse> GetPurchaseInInstallments(GetPurchaseInInstallmentsRequest getPurchaseInInstallmentsRequest)
        {
            PurchaseInInstallments? purchaseInInstallments = await purchaseInInstallmentsRepositorie.GetPurchaseInInstallmentsAsync(getPurchaseInInstallmentsRequest.PurchaseInInstallmentsId);

            if (purchaseInInstallments == null)
                return new BaseResponse(new List<string>() { "Pagamento não encontrado" });

            return new BaseResponse<PurchaseInInstallmentsModel>(purchaseInInstallments.MapperEntitieToModel());
        }

        public async Task<BaseResponse> GetPurchaseInInstallmentsListActive(GetPurchaseInInstallmentsListActiveRequest getPurchaseInInstallmentsListActiveRequest)
        {
            BaseResponse cardBaseResponse = await cardService.GetCardAsync(new GetCardRequest() { CardId = getPurchaseInInstallmentsListActiveRequest.CardId, UserId = getPurchaseInInstallmentsListActiveRequest .UserId});

            if (!cardBaseResponse.Success)
                return new BaseResponse(new List<string>() { "Cartão não encontrado!" });

            CardModel cardModel = cardBaseResponse.GetEntitie<CardModel>();

            List<PurchaseInInstallments>? installments = await purchaseInInstallmentsRepositorie.GetPurchaseInInstallmentsActiveAsync(cardModel.Id);

            if (!installments.Any())
                return new BaseResponse(new List<string>() { "Não possui dividas ativas!" });

            List<PurchaseInInstallmentsModel> purchaseInInstallmentsModelList = installments.Select(x => x.MapperEntitieToModel()).ToList();

            return new BaseResponse<GetPurchaseInInstallmentsListActiveModel>(new GetPurchaseInInstallmentsListActiveModel()
            {
                CardModel = cardModel,
                PurchaseInInstallmentsModels = purchaseInInstallmentsModelList
            });
        }

        private async Task InsertInstallmentsAsync(PurchaseInInstallments purchaseInInstallments, int index, decimal installmentValue)
        {
            Installments installments = new Installments
            {
                PurchaseInInstallmentsId = purchaseInInstallments.Id,
                InstallmentNumber = index + 1,
                ReferringMonth = purchaseInInstallments.DateOfPurchase.AddMonths(index),
                Value = installmentValue,
                Paid = false
            };

            await installmentsRepositorie.InsertPurchaseInInstallmentsAsync(installments);
        }

        private async Task InsertCustomizedInstallmentAsync(PurchaseInInstallments purchaseInInstallments, CustomizedInstallment customizedInstallment)
        {
            Installments installments = new Installments
            {
                PurchaseInInstallmentsId = purchaseInInstallments.Id,
                InstallmentNumber = customizedInstallment.InstallmentNumber,
                ReferringMonth = customizedInstallment.DueDate,
                Value = customizedInstallment.Value,
                Paid = customizedInstallment.Paid
            };

            await installmentsRepositorie.InsertPurchaseInInstallmentsAsync(installments);
        }
    }
}
