using Debts.API.EndPoints;
using FluentValidation;
using Integrations.Client;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddScoped<AppClient>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Add services to the container.
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.MapPurchaseEndPoints(app.Configuration);

app.Run();