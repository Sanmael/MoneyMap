using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IInstallmentsRepositorie
    {
        public Task InsertPurchaseInInstallmentsAsync(Installments baseEntitie);
        public Task<List<Installments?>> GetActiveInstallmentsByMonthAsync(Guid userId, DateTime? startMonth, DateTime? endMonth);
    }
}
