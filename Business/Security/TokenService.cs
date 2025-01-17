using Business.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Business.Security
{
    //ajustar depois string mockadas, e chave commitada rs
    public class TokenService
    {
        public static ClaimsPrincipal ReadJsonJWT(string token)
        {
            string secretKey = "d91b999e55fe40528dd0ae08c572c771";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateLifetime = true
                };

                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                return principal;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token inválido: {ex.Message}");
                throw;
            }
        }

        public static ClaimsDTO GetUserData(HttpContext httpContext)
        {
            string token = GetAccessToken(httpContext);
            ClaimsPrincipal claim = ReadJsonJWT(token);

            return new ClaimsDTO()
            {
                UserId = Guid.Parse(claim.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value),
                Email = claim.Claims.First(claim => claim.Type == ClaimTypes.Email).Value,
                UserName = claim.Claims.First(claim => claim.Type == ClaimTypes.Name).Value,
                Token = token
            };
        }

        public static string GetAccessToken(HttpContext httpContext)
        {
            var token = httpContext.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
                token = token.Substring("Bearer ".Length).Trim();

            else
                throw new UnauthorizedAccessException("Token não encontrado ou inválido.");

            return token;
        }
    }
}
