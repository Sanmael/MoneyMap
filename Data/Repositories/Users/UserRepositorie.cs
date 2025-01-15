using Data.Context;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Users
{
    public class UserRepositorie(IBaseRepositorie<User> baseRepositorie, EntityFramework entityFramework) : IUserRepository
    {
        public async Task<bool> AddUserAsync(User user)
        {
            await baseRepositorie.InsertAsync(user);
            return true;
        }

        public async Task<User?> GetUserByEmail(string userName)
        {
            User? user = await entityFramework.User.FirstOrDefaultAsync(x => x.UserName == userName);

            return user;
        }

        public bool ValidateIfExistUser(string email)
        {
            return entityFramework.User.Any(x => x.Email == email);
        }
    }
}