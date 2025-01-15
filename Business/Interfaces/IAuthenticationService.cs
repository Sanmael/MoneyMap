using Business.Response;

namespace Business.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<BaseResponse> LoginAsync(string email, string password);        
        public Task<bool> IsTokenValidAsync(Guid userId, string token);
        public Task<bool> LogOut(Guid userId);
        public string HashPassword(string password);
        public string GenerateToken(string email, string userName, Guid id);
        public bool VerifyPassword(string enteredPassword, string storedHash);
    }
}