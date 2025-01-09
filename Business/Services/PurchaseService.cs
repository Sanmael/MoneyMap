using Business.Extensions;
using Business.Interfaces;
using Business.Models;
using Business.Requests;
using Business.Requests.Purchase;
using Business.Response;
using Domain.Base.Entities;
using Domain.Base.Interfaces.Repositories;

namespace Business.Services
{
    public class PurchaseService(IPurchaseRepositorie purchaseRepositorie) : IPurchaseService
    {
        public async Task<BaseResponse> AddPurchaseAsync(InsertPurchaseRequest purchaseInInstallmentsRequest)
        {
            Purchase purchase = purchaseInInstallmentsRequest.MapperInsertRequestToEntitie();

            await purchaseRepositorie.InsertPurchaseAsync(purchase);

            return new BaseResponse<Guid>(purchase.Id);
        }

        public async Task<BaseResponse> GetPurchase(GetPurchaseRequest getPurchaseRequest)
        {
            Purchase? purchase = await purchaseRepositorie.GetPurchaseAsync(getPurchaseRequest.PurchaseId);

            if (purchase == null)
                return new BaseResponse("Compra inexistente");

            PurchaseModel purchaseModel = purchase.MapperEntitieToModel();

            return new BaseResponse<PurchaseModel>(purchaseModel);
        }

        public async Task<BaseResponse> GetPurchaseListActive(BaseRequest baseRequest)
        {
            List<Purchase>? purchases = await purchaseRepositorie.GetPurchaseActiveAsync(baseRequest.UserId);

            if (purchases == null || !purchases.Any())
                return new BaseResponse("Não existem compras ativas");

            List<PurchaseModel> purchaseModels = purchases.Select(x => x.MapperEntitieToModel()).ToList();

            return new BaseResponse<List<PurchaseModel>>(purchaseModels);
        }
    }
}
