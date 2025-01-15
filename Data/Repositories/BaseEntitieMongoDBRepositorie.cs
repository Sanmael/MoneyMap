using Data.Context;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using MongoDB.Driver;

namespace Data.Repositories
{
    public class BaseEntitieMongoDBRepositorie<T>(MongoDBContext mongoDBContext) : IBaseRepositorie<T> where T : BaseEntitie
    {        
        public async Task Delete(T baseEntitie)
        {
            IMongoCollection<T> mongoCollection = mongoDBContext.GetCollection<T>();

            await mongoCollection.DeleteOneAsync(x=> x.Id == baseEntitie.Id);
        }

        public async Task InsertAsync(T baseEntitie)
        {                     
            IMongoCollection<T> mongoCollection = mongoDBContext.GetCollection<T>();
            await mongoCollection.InsertOneAsync(baseEntitie);
        }

        public async Task Update(T baseEntitie)
        {
            IMongoCollection<T> mongoCollection = mongoDBContext.GetCollection<T>();

            FilterDefinition<T> filter = Builders<T>.Filter.Eq(e => e.Id, baseEntitie.Id);

            baseEntitie.UpdatedAt = DateTime.Now;
            ReplaceOneResult result = await mongoCollection.ReplaceOneAsync(filter, baseEntitie);
        }
    }
}