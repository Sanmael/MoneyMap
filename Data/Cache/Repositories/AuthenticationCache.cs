using Domain.Interfaces.Repositories;

namespace Data.Cache.Repositories
{
    public class AuthenticationCache(RedisCacheRepositorie redisCacheRepositorie) : IAuthenticationRepositorie
    {        
        public async Task<string> GetAuthenticationToken(Guid userId)
        {
            return await redisCacheRepositorie.GetCache<string>(userId.ToString());
        }
       
        public async Task<bool> LogOut(Guid userId)
        {
            await redisCacheRepositorie.RemoveCache(userId.ToString(), nameof(GetAuthenticationToken));
            return true;
        }

        public async Task SaveAuthenticationToken(string token, Guid userId)
        {
            string cache = await redisCacheRepositorie.GetCache<string>(userId.ToString(), nameof(GetAuthenticationToken));

            if (cache != null)
                await redisCacheRepositorie.RemoveCache(cache);

            await redisCacheRepositorie.SaveCache(token, userId.ToString(), nameof(GetAuthenticationToken));
        }
    }
}