using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Data.Context
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(IOptions<MongoDbConfig> mongoDbConfig)
        {
            MongoClient client = new MongoClient(mongoDbConfig.Value.ConnectionString);
            _database = client.GetDatabase(mongoDbConfig.Value.DatabaseName);
        }
        
        public IMongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(typeof(T).Name);
        }        
    }
}