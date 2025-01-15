using Business.Interfaces;
using Business.Requests.User;
using Business.Response;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Business.Services
{
    public class UserService
        (IAuthenticationService authenticationService, IUserRepository userRepository) : IUserService
    {       
        public async Task<BaseResponse> SaveNewUser(CreateUserRequest request)
        {
            User user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Age = request.Age,
                BirthDate = request.BirthDate,
                Occupation = request.Occupation,
                Salary = request.Salary,
                Balance = request.Balance,
                UserName = request.UserName
            };

            user.Password = authenticationService.HashPassword(request.Password);

            bool success = await userRepository.AddUserAsync(user);

            return new BaseResponse<bool>(success);
        }
        
    }
}