using Business.Interfaces;
using Business.Services;
using Data.Context;
using Data.Repositories;
using Data.Repositories.Cards;
using Domain.Base.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// Add services to the container.
builder.Services.AddDbContext<EntityFramework>(dboptions =>
{
    dboptions.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConfig"));
});

builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IPurchaseInInstallmentsService, PurchaseInInstallmentsService>();
builder.Services.AddScoped<CardRepositorie>();
builder.Services.AddScoped<InstallmentsRepositorie>();
builder.Services.AddScoped<PurchaseInInstallmentsRepositorie>();
builder.Services.AddScoped(typeof(IBaseRepositorie<>), typeof(BaseEntitieFrameworkRepositorie<>));

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();
app.MapControllers();
app.Run();