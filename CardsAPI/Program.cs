using Data.Context;
using Microsoft.EntityFrameworkCore;
using CardsAPI.EndPoints;
using CardsAPI.Dependencies;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EntityFramework>(dboptions =>
{
    dboptions.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConfig"));
});

builder.Services.AddCardDependencies();

//WEB APP
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.MapCardEndPoints();
app.MapPurchaseInInstallmentsEndPoints();
app.Run();