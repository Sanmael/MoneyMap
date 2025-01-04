using Business.Interfaces;
using Business.Services;
using Data.Repositories.Cards;
using Data.Repositories;
using Domain.Base.Interfaces;
using FluentValidation;
using Business.Handlers;

namespace CardsAPI.Dependencies
{
    public static class CardDependencies
    {
        public static void AddCardDependencies(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<Program>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IPurchaseInInstallmentsService, PurchaseInInstallmentsService>();
            services.AddScoped<CardRepositorie>();
            services.AddScoped<InstallmentsRepositorie>();
            services.AddScoped<PurchaseInInstallmentsRepositorie>();
            services.AddScoped(typeof(IBaseRepositorie<>), typeof(BaseEntitieFrameworkRepositorie<>));
            services.AddExceptionHandler<CustomExceptionHandler>();
        }
    }
}