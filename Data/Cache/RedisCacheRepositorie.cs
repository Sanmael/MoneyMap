using Microsoft.Extensions.Caching.Distributed;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Data.Cache
{
    public class RedisCacheRepositorie(IDistributedCache distributedCache)
    {
        public async Task SaveCache<T>(T entity, string id, [CallerMemberName] string callerName = "")
        {
            string serializedEntity = JsonSerializer.Serialize(entity, GetOptions());
            string key = callerName + id;
            await distributedCache.SetStringAsync(key, serializedEntity);
        }

        public async Task<T?> GetCache<T>(string id, [CallerMemberName] string callerName = "")
        {
            string key = callerName + id;
            string? cached = await distributedCache.GetStringAsync(key);           
            return !string.IsNullOrEmpty(cached) ? JsonSerializer.Deserialize<T>(cached!, GetOptions()) : default;
        }

        //TODO : Remover metodo apos corrigir a ciclagem nos objetos
        private JsonSerializerOptions GetOptions()
        {
            return new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
        }

        public async Task RemoveCache(string id)
        {            
            await distributedCache.RemoveAsync(id);
        }
    }
}