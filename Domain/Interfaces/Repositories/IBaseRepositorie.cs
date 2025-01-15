using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IBaseRepositorie<T> where T : BaseEntitie
    {
        public Task InsertAsync(T baseEntitie);
        public Task Update(T baseEntitie);
        public Task Delete(T baseEntitie);
    }
}