using Data.Context;
using Domain.Base.Entities;
using Domain.Base.Interfaces.Repositories;
using Domain.Cards.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Data.Repositories.Cards
{
    public class PurchaseInInstallmentsRepositorie(IBaseRepositorie<PurchaseInInstallments> baseRepositorie, MongoDBContext mongoDBContext)
        : IPurchaseInInstallmentsRepositorie
    {
        public async Task DeletePurchaseInInstallmentsAsync(PurchaseInInstallments baseEntitie)
        {
            await baseRepositorie.Delete(baseEntitie);
        }

        public async Task InsertPurchaseInInstallmentsAsync(PurchaseInInstallments baseEntitie)
        {
            await baseRepositorie.InsertAsync(baseEntitie);
        }

        public async Task UpdatePurchaseInInstallmentsAsync(PurchaseInInstallments baseEntitie)
        {
            await baseRepositorie.Update(baseEntitie);
        }

        public async Task<PurchaseInInstallments?> GetPurchaseInInstallmentsAsync(Guid purchaseInInstallmentsId)
        {
            var query = new[]
            {
                new BsonDocument("$match", new BsonDocument
                {
                    { "_id", new BsonBinaryData(purchaseInInstallmentsId, GuidRepresentation.Standard) },
                }),
                new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "Installments" },
                    { "localField", "_id" },
                    { "foreignField", "PurchaseInInstallmentsId" },
                    { "as", "Installments" }
                })
            };

            var result = await mongoDBContext.GetCollection<PurchaseInInstallments>()
                   .AggregateAsync<PurchaseInInstallments>(query);

            return await result.FirstAsync();
        }

        public async Task<List<PurchaseInInstallments>>? GetPurchaseInInstallmentsActiveByCardIdAsync(Guid cardId)
        {
            var query = new[]
            {
                new BsonDocument("$match", new BsonDocument
                {
                    { "CardId", new BsonBinaryData(cardId, GuidRepresentation.Standard) },
                    { "Paid", false }
                }),
                new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "Installments" },
                    { "localField", "_id" },
                    { "foreignField", "PurchaseInInstallmentsId" },
                    { "as", "Installments" }
                })
            };

            var result = await mongoDBContext.GetCollection<PurchaseInInstallments>()
                    .AggregateAsync<PurchaseInInstallments>(query);

            return await result.ToListAsync();
        }

        public async Task<List<PurchaseInInstallments>>? GetAllPurchaseInInstallmentsActiveAsync(Guid userId)
        {
            var query = new[]
            {
                new BsonDocument("$match", new BsonDocument
                {
                    { "UserId", new BsonBinaryData(userId, GuidRepresentation.Standard) },
                    { "Paid", false }
                }),
                new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "Installments" },
                    { "localField", "_id" },
                    { "foreignField", "PurchaseInInstallmentsId" },
                    { "as", "Installments" }
                })
            };

            var result = await mongoDBContext
                .GetCollection<PurchaseInInstallments>()
                .AggregateAsync<PurchaseInInstallments>(query);

            return await result.ToListAsync();
        }

        public async Task<List<PurchaseInInstallments>> GetAllPurchaseInInstallmentsByDateAsync(Guid userId,DateTime? firstDate, DateTime? lastDate)
        {
            if (firstDate == null && lastDate == null)
                throw new ArgumentException("Deve haver ao menos um filtro preenchido.");

            var filters = new List<BsonDocument>
            {
                new BsonDocument("$match", new BsonDocument 
                { 
                    { "Paid", false },
                    { "UserId", new BsonBinaryData(userId, GuidRepresentation.Standard) },
                })
            };

            if (firstDate != null)
            {
                filters.Add(new BsonDocument("$match", new BsonDocument
                 {
                   { "DateOfPurchase", new BsonDocument("$gte", firstDate.Value) }
                 }));
            }

            if (lastDate != null)
            {
                filters.Add(new BsonDocument("$match", new BsonDocument
                {
                    { "DateOfPurchase", new BsonDocument("$lte", lastDate.Value) }
                }));
            }

            filters.Add(new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "Installments" },
                { "localField", "_id" },
                { "foreignField", "PurchaseInInstallmentsId" },
                { "as", "Installments" }
            }));

            var result = await mongoDBContext
                .GetCollection<PurchaseInInstallments>()
                .AggregateAsync<PurchaseInInstallments>(filters);

            return await result.ToListAsync();
        }
    }
}