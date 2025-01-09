using Data.Context;
using Domain.Base.Entities;
using MongoDB.Driver;
using Domain.Base.Interfaces.Repositories;

namespace Data.Repositories.Purchases
{
    public class PurchaseRepositorie(IBaseRepositorie<Purchase> baseRepositorie,
        MongoDBContext mongoDBContext) : IPurchaseRepositorie
    {
        public async Task InsertPurchaseAsync(Purchase purchase)
        {
            await baseRepositorie.InsertAsync(purchase);
        }

        public async Task UpdatePurchaseAsync(Purchase baseEntitie)
        {
            await baseRepositorie.Update(baseEntitie);
        }

        public async Task<Purchase?> GetPurchaseAsync(Guid purchaseId)
        {
            IMongoCollection<Purchase> mongoCollection = mongoDBContext.GetCollection<Purchase>();

            var document = await mongoCollection.FindAsync(x=> x.Id == purchaseId);

            return document.First();            
        }

        public async Task<List<Purchase>> GetPurchaseActiveAsync(Guid userId)
        {
            IMongoCollection<Purchase> mongoCollection = mongoDBContext.GetCollection<Purchase>();

            var document = await mongoCollection.FindAsync(x => x.UserId == userId && !x.Paid);            

            return document.ToList();
        }
    }
}