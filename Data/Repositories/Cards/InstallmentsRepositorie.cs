using Data.Context;
using Domain.Base.Interfaces.Repositories;
using Domain.Cards.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Data.Repositories.Cards
{
    public class InstallmentsRepositorie(IBaseRepositorie<Installments> baseRepositorie, MongoDBContext mongoDBContext) : IInstallmentsRepositorie
    {
        public async Task InsertPurchaseInInstallmentsAsync(Installments baseEntitie)
        {
            await baseRepositorie.InsertAsync(baseEntitie);
        }

        public async Task<List<Installments>> GetActiveInstallmentsByMonthAsync(Guid userId, DateTime? startMonth, DateTime? endMonth)
        {
            var filters = new List<BsonDocument>
    {
        new BsonDocument("$lookup", new BsonDocument
        {
            { "from", "PurchaseInInstallments" },
            { "localField", "PurchaseInInstallmentsId" },
            { "foreignField", "_id" },
            { "as", "PurchaseInInstallments" }
        }),
        new BsonDocument("$unwind", new BsonDocument
        {
            { "path", "$PurchaseInInstallments" },
            { "preserveNullAndEmptyArrays", false }
        }),
        new BsonDocument("$match", new BsonDocument
        {
            { "PurchaseInInstallments.UserId", new BsonBinaryData(userId, GuidRepresentation.Standard) },
            { "Paid", false }
        })
    };

            if (startMonth.HasValue)
            {
                filters.Add(new BsonDocument("$match", new BsonDocument
        {
            { "ReferringMonth", new BsonDocument("$gte", startMonth.Value) }
        }));
            }

            if (endMonth.HasValue)
            {
                filters.Add(new BsonDocument("$match", new BsonDocument
        {
            { "ReferringMonth", new BsonDocument("$lte", endMonth.Value) }
        }));
            }

            var result = await mongoDBContext
                .GetCollection<Installments>()
                .AggregateAsync<Installments>(filters);

            return await result.ToListAsync();
        }
    }
}