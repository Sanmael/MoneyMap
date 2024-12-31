using Data.Context;
using Domain.Base.Entities;
using Domain.Base.Interfaces;

namespace Data.Repositories
{
    public class BaseEntitieFrameworkRepositorie<T>(EntityFramework entityFramework) : IBaseRepositorie<T> where T : BaseEntitie
    {        
        public async Task Delete(T baseEntitie)
        {
            entityFramework.Set<T>().Remove(baseEntitie);
            await entityFramework.SaveChangesAsync();
        }

        public async Task InsertAsync(T baseEntitie)
        {
            await entityFramework.Set<T>().AddAsync(baseEntitie);
            await entityFramework.SaveChangesAsync();
        }

        public async Task Update(T baseEntitie)
        {
            entityFramework.Set<T>().Update(baseEntitie);
            await entityFramework.SaveChangesAsync();
        }
    }
}