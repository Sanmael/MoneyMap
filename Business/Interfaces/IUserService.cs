using Business.Requests.User;
using Business.Response;

namespace Business.Interfaces
{
    public interface IUserService
    {        
        public Task<BaseResponse> SaveNewUser(CreateUserRequest request);
    }
}