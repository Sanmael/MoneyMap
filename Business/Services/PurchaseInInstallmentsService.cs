using Business.Extensions;
using Business.Interfaces;
using Business.Models;
using Business.Requests;
using Business.Requests.Card;
using Business.Requests.Card.PurchaseInInstallments;
using Business.Response;
using Domain.Base.Interfaces.Repositories;
using Domain.Cards.Entities;

namespace Business.Services
{
    public class PurchaseInInstallmentsService(ICardService cardService, IPurchaseInInstallmentsRepositorie purchaseInInstallmentsRepositorie,
        IInstallmentsRepositorie installmentsRepositorie) : IPurchaseInInstallmentsService
    {
        public async Task<BaseResponse> AddPurchaseInInstallmentsAsync(InsertPurchaseInInstallmentsRequest purchaseInInstallmentsRequest)
        {
            BaseResponse response = await cardService.GetCardAsync(new GetCardRequest()
            {
                CardId = purchaseInInstallmentsRequest.CardId,
                UserId = purchaseInInstallmentsRequest.UserId
            });

            if (!response.Success)
                return new BaseResponse("cartão não encontrado ou não pertence ao usuario");

            PurchaseInInstallments purchaseInInstallments = purchaseInInstallmentsRequest.MapperInsertRequestToEntitie();

            CardModel card = response.GetData<CardModel>();

            decimal newBalance = card.Balance - purchaseInInstallments.TotalPrice;

            await cardService.UpdateBalanceCardAsync(new UpdateBalanceCardRequest() { Balance = newBalance, CardId = card.Id, UserId = card.UserId });

            await purchaseInInstallmentsRepositorie.InsertPurchaseInInstallmentsAsync(purchaseInInstallments);

            if (purchaseInInstallmentsRequest.CustomInstallments != null && purchaseInInstallmentsRequest.CustomInstallments.Any())
            {
                decimal totalCustomInstallments = purchaseInInstallmentsRequest.CustomInstallments.Sum(x => x.Value);

                if (totalCustomInstallments != purchaseInInstallments.TotalPrice)
                    return new BaseResponse("O total das parcelas personalizadas não coincide com o valor total da compra.");

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

            return new BaseResponse<Guid>(purchaseInInstallments.Id, "Pagamento no cartão registrado com sucesso");
        }

        public async Task<BaseResponse> GetPurchaseInInstallments(GetPurchaseInInstallmentsRequest getPurchaseInInstallmentsRequest)
        {
            PurchaseInInstallments? purchaseInInstallments = await purchaseInInstallmentsRepositorie.GetPurchaseInInstallmentsAsync(getPurchaseInInstallmentsRequest.PurchaseInInstallmentsId);

            if (purchaseInInstallments == null)
                return new BaseResponse("Pagamento não encontrado");

            return new BaseResponse<PurchaseInInstallmentsModel>(purchaseInInstallments.MapperEntitieToModel());
        }

        public async Task<BaseResponse> GetPurchaseInInstallmentsListActive(GetPurchaseInInstallmentsListActiveRequest getPurchaseInInstallmentsListActiveRequest)
        {
            BaseResponse cardBaseResponse = await cardService.GetCardAsync(new GetCardRequest() { CardId = getPurchaseInInstallmentsListActiveRequest.CardId, UserId = getPurchaseInInstallmentsListActiveRequest.UserId });

            if (!cardBaseResponse.Success)
                return new BaseResponse("Cartão não encontrado!");

            CardModel cardModel = cardBaseResponse.GetData<CardModel>();

            List<PurchaseInInstallments>? installments = await purchaseInInstallmentsRepositorie.GetPurchaseInInstallmentsActiveByCardIdAsync(cardModel.Id);

            if (!installments.Any())
                return new BaseResponse("Não possui dividas ativas!");

            List<PurchaseInInstallmentsModel> purchaseInInstallmentsModelList = installments.Select(x => x.MapperEntitieToModel()).ToList();

            return new BaseResponse<GetPurchaseInInstallmentsListActiveModel>(new GetPurchaseInInstallmentsListActiveModel()
            {
                TotalDebt = purchaseInInstallmentsModelList.Select(x => x.InstallmentsModels.Where(installment => !installment.Paid).
                Sum(installment => installment.Value)).Sum(),
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

        public async Task<BaseResponse> GetAllPurchaseInInstallmentsListActive(BaseRequest baseRequest)
        {
            List<PurchaseInInstallments>? installments = await purchaseInInstallmentsRepositorie.GetAllPurchaseInInstallmentsActiveAsync(baseRequest.UserId);
            List<PurchaseInInstallmentsModel>? purchaseInInstallmentsModels = installments.Select(entity => entity.MapperEntitieToModel()).ToList();

            BaseResponse response = await cardService.GetAllCardAsync(new GetAllCardsRequest() { UserId = baseRequest.UserId });

            if (!response.Success)
                return response;

            List<CardModel> cardModels = response.GetData<List<CardModel>>();

            foreach (var cardModel in cardModels)
            {                
                cardModel.PurchaseInInstallmentsModels = purchaseInInstallmentsModels.Where(x=> x.CardId == cardModel.Id).ToList(); 
            }

            return new BaseResponse<List<CardModel>>(cardModels);          
        }

        public async Task<BaseResponse> GetAllPurchaseInInstallmentsListActiveByDate(GetAllPurchaseInInstallmentsListActiveByDateRequest getAllPurchaseInInstallmentsListActiveByDateRequest)
        {
            List<PurchaseInInstallments>? installments = await purchaseInInstallmentsRepositorie.GetAllPurchaseInInstallmentsByDateAsync
                (getAllPurchaseInInstallmentsListActiveByDateRequest.UserId,getAllPurchaseInInstallmentsListActiveByDateRequest.FirstDate,getAllPurchaseInInstallmentsListActiveByDateRequest.LastDate);

            List<PurchaseInInstallmentsModel>? purchaseInInstallmentsModels = installments.Select(entity => entity.MapperEntitieToModel()).ToList();

            BaseResponse response = await cardService.GetAllCardAsync(new GetAllCardsRequest() { UserId = getAllPurchaseInInstallmentsListActiveByDateRequest.UserId });

            if (!response.Success)
                return response;

            List<CardModel> cardModels = response.GetData<List<CardModel>>();

            foreach (var cardModel in cardModels)
            {
                cardModel.PurchaseInInstallmentsModels = purchaseInInstallmentsModels.Where(x => x.CardId == cardModel.Id).ToList();
            }

            return new BaseResponse<List<CardModel>>(cardModels);
        }

        public async Task<BaseResponse> GetActiveInstallmentsByMonthAsync(GetActiveInstallmentsByMonthAsyncRequest getActiveInstallmentsByMonthAsyncRequest)
        {
            List<Installments?> installments = await installmentsRepositorie.GetActiveInstallmentsByMonthAsync(getActiveInstallmentsByMonthAsyncRequest.UserId, getActiveInstallmentsByMonthAsyncRequest.StartMonth, getActiveInstallmentsByMonthAsyncRequest.EndMonth);

            var groupedInstallments = installments
                .Where(x => x != null)
                .GroupBy(x => x!.PurchaseInInstallmentsId)
                .Select(group =>
                {
                    var purchaseInInstallments = group.First()!.PurchaseInInstallments;
                    var installmentsList = group.ToList();
                    return purchaseInInstallments.MapperEntitieToModel(installmentsList);

                }).ToList();

            BaseResponse response = await cardService.GetAllCardAsync(new GetAllCardsRequest() { UserId = getActiveInstallmentsByMonthAsyncRequest.UserId });

            if (!response.Success)
                return response;

            List<CardModel> cardModels = response.GetData<List<CardModel>>();

            foreach (var cardModel in cardModels)
            {
                cardModel.PurchaseInInstallmentsModels = groupedInstallments.Where(x => x.CardId == cardModel.Id).ToList();
            }

            return new BaseResponse<List<CardModel>>(cardModels);
        }
    }
}