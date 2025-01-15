using Business.Interfaces;
using Business.Services;
using Data.Repositories;
using FluentValidation;
using Business.Handlers;
using Data.Repositories.Purchases;
using Domain.Interfaces.Repositories;

namespace PurchaseAPI.Dependencies
{
    public static class PurchaseDependencies
    {
        public static void AddPurchaseDependencies(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<Program>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IPurchaseRepositorie, PurchaseRepositorie>();            
            services.AddScoped(typeof(IBaseRepositorie<>), typeof(BaseEntitieMongoDBRepositorie<>));
            services.AddExceptionHandler<CustomExceptionHandler>();
        }
    }
}