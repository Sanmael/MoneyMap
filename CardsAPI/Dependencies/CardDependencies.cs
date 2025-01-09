using Business.Interfaces;
using Business.Services;
using Data.Repositories.Cards;
using Data.Repositories;
using FluentValidation;
using Business.Handlers;
using Data.Context;
using Domain.Cards.Entities;
using Domain.Base.Interfaces.Repositories;
using Data.Cache;
using Data.Cache.Repositories;

namespace CardsAPI.Dependencies
{
    public static class CardDependencies
    {
        public static void AddCardDependencies(this IServiceCollection services)
        {
            services.AddScoped<CardRepositorie>(provider =>
            {
                var entity = provider.GetRequiredService<EntityFramework>();

                return new CardRepositorie
                (                    
                    new BaseEntitieFrameworkRepositorie<Card>(entity),
                    entity
                );
            });

            services.AddValidatorsFromAssemblyContaining<Program>();
            services.AddScoped<ICardService,CardService>();            
            services.AddScoped<IPurchaseInInstallmentsService, PurchaseInInstallmentsService>();
            services.AddScoped<IInstallmentsRepositorie, InstallmentsRepositorie>();
            services.AddScoped<PurchaseInInstallmentsRepositorie>();
            services.AddScoped<RedisCacheRepositorie>();

            services.AddScoped<IPurchaseInInstallmentsRepositorie>(config =>
            {
                PurchaseInInstallmentsRepositorie purchaseInInstallmentsRepositorie = config.GetRequiredService<PurchaseInInstallmentsRepositorie>();
                RedisCacheRepositorie purchaseInInstallmentsRepositorieCache = config.GetRequiredService<RedisCacheRepositorie>();
                return new PurchaseInInstallmentsCache(purchaseInInstallmentsRepositorie, purchaseInInstallmentsRepositorieCache);
            });

            services.AddScoped<ICardRepositorie>(config =>
            {
                CardRepositorie cardRepositorie = config.GetRequiredService<CardRepositorie>();
                RedisCacheRepositorie redisCacheRepositorie = config.GetRequiredService<RedisCacheRepositorie>();
                return new CardCache(cardRepositorie, redisCacheRepositorie);
            });


            services.AddScoped(typeof(IBaseRepositorie<>), typeof(BaseEntitieMongoDBRepositorie<>));
            services.AddExceptionHandler<CustomExceptionHandler>();
        }
    }
}