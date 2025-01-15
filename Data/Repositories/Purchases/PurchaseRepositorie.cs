using Data.Context;
using MongoDB.Driver;
using Domain.Entities;
using Domain.Interfaces.Repositories;

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

            var document = await mongoCollection.FindAsync(x => x.Id == purchaseId);

            return document.First();
        }

        public async Task<List<Purchase>> GetPurchaseActiveAsync(Guid userId)
        {
            IMongoCollection<Purchase> mongoCollection = mongoDBContext.GetCollection<Purchase>();

            var document = await mongoCollection.FindAsync(x => x.UserId == userId && !x.Paid);

            return document.ToList();
        }

        public async Task<List<Purchase>> GetPurchaseActiveByDate(Guid userId, DateTime? firstDate, DateTime? lastDate)
        {
            if (firstDate == null && lastDate == null)
                throw new ArgumentException("Deve haver ao menos um filtro preenchido.");

            IMongoCollection<Purchase> mongoCollection = mongoDBContext.GetCollection<Purchase>();

            FilterDefinition<Purchase> filters = Builders<Purchase>.Filter.Eq(x => x.UserId, userId);

            if (firstDate != null)
                filters &= Builders<Purchase>.Filter.Gte(x => x.DateOfPurchase, firstDate.Value);

            if (lastDate != null)
                filters &= Builders<Purchase>.Filter.Lte(x => x.DateOfPurchase, lastDate.Value);

            return await mongoCollection.Find(filters).ToListAsync();
        }
    }
}