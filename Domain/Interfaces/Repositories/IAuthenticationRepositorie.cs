namespace Domain.Interfaces.Repositories
{
    public interface IAuthenticationRepositorie
    {
        public Task SaveAuthenticationToken(string token, Guid userId);
        public Task<string?> GetAuthenticationToken(Guid userId);
        public Task<bool> LogOut(Guid userId);
    }
}
