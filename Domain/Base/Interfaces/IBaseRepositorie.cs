using Domain.Base.Entities;

namespace Domain.Base.Interfaces
{
    public interface IBaseRepositorie<T> where T : BaseEntitie
    {
        public Task InsertAsync(T baseEntitie);
        public Task Update(T baseEntitie);
        public Task Delete(T baseEntitie);
    }
}