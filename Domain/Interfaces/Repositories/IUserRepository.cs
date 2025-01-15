using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> AddUserAsync(User user);
        public Task<User?> GetUserByEmail(string email);
        public bool ValidateIfExistUser(string email);
    }
}