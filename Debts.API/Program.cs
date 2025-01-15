using Data.Context;
using Debts.API.EndPoints;
using FluentValidation;
using Integrations.Client;
using Microsoft.EntityFrameworkCore;
using Business.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddScoped<AppClient>();
builder.Services.AddAuthorization();
builder.Services.AddCors();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.AddJwtAuthorize();

builder.Services.AddDbContext<EntityFramework>(dboptions =>
{
    dboptions.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConfig"));
});

// Add services to the container.
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapPurchaseEndPoints(app.Configuration);

app.Run();