using Business.Interfaces;
using Business.Response;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Business.Services
{
    public class AuthenticationService(IUserRepository userRepository, IAuthenticationRepositorie authenticationRepositorie,  IConfiguration configuration) : IAuthenticationService
    {
        public string HashPassword(string password)
        {
            byte[] salt = GenerateSalt();

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA1))
            {
                byte[] hash = pbkdf2.GetBytes(20);
                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);

                return Convert.ToBase64String(hashBytes);
            }
        }

        private byte[] GenerateSalt()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] salt = new byte[16];
                rng.GetBytes(salt);
                return salt;
            }
        }

        public bool VerifyPassword(string enteredPassword, string storedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            byte[] salt = new byte[16];

            Array.Copy(hashBytes, 0, salt, 0, 16);

            byte[] storedPasswordHash = new byte[20];

            Array.Copy(hashBytes, 16, storedPasswordHash, 0, 20);

            using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000, HashAlgorithmName.SHA1))
            {
                byte[] enteredPasswordHash = pbkdf2.GetBytes(20);

                for (int i = 0; i < 20; i++)
                {
                    if (enteredPasswordHash[i] != storedPasswordHash[i])
                        return false;
                }
            }

            return true;
        }

        public string GenerateToken(string email, string userName, Guid id)
        {
            byte[] key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("Key"));

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email,email),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.NameIdentifier, id.ToString())
                }),

                Expires = DateTime.UtcNow.AddDays(7),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<BaseResponse> LoginAsync(string email, string password)
        {
            User? user = await userRepository.GetUserByEmail(email);

            if (user == null)
                return new BaseResponse("Usuario não encontrado");

            bool validatePassword = VerifyPassword(password, user.Password);

            if (!validatePassword)
                return new BaseResponse("Senha invalida");

            string token = GenerateToken(user.Email, user.UserName, user.Id);

            await authenticationRepositorie.SaveAuthenticationToken(token, user.Id);

            return new BaseResponse<string>(token);
        }

        public async Task<bool> IsTokenValidAsync(Guid userId, string token)
        {
            string? dbToken = await authenticationRepositorie.GetAuthenticationToken(userId);
            return dbToken == token;
        }
        public async Task<bool> LogOut(Guid userId)
        {
            return await authenticationRepositorie.LogOut(userId);
        }
    }
}