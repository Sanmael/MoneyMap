using Business.Interfaces;
using Business.Services;
using Data.Cache.Repositories;
using Data.Cache;
using Data.Repositories.Users;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Domain.Interfaces.Repositories;

namespace Business.Extensions
{
    public static class WebApplicationExtension
    {
        public static void AddJwtAuthorize(this WebApplicationBuilder builder)
        {
            builder.Services.AddStackExchangeRedisCache(config =>
            {
                config.Configuration = builder.Configuration.GetConnectionString("Redis");
            });

            builder.AddUserDependencies();

            var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("Key"));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        string? userId = context?.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                        string? token = context?.SecurityToken.UnsafeToString();                        

                        if (userId == null || token == null)
                        {
                            context?.Fail("Token inválido.");
                            return;
                        }

                        IAuthenticationService authService = context.HttpContext.RequestServices.GetRequiredService<IAuthenticationService>();

                        bool isValid = await authService.IsTokenValidAsync(Guid.Parse(userId), token);

                        if (!isValid)
                        {
                            context.Fail("Token inválido.");
                        }
                    }
                };
            });

            builder.Services.AddAuthorization();
        }

        public static void AddUserDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped(typeof(IBaseRepositorie<>), typeof(BaseEntitieFrameworkRepositorie<>));
            builder.Services.AddScoped<AuthenticationService>();
            builder.Services.AddScoped<UserRepositorie>();
            builder.Services.AddScoped<RedisCacheRepositorie>();
            builder.Services.AddScoped<IUserRepository, UserRepositorie>();
            builder.Services.AddScoped<IAuthenticationRepositorie, AuthenticationCache>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        }
    }
}