using Data.Context;
using Microsoft.EntityFrameworkCore;
using CardsAPI.EndPoints;
using CardsAPI.Dependencies;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Business.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EntityFramework>(dboptions =>
{
    dboptions.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConfig"));
});

builder.Services.AddStackExchangeRedisCache(config =>
{
    config.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.AddJwtAuthorize();

builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection("MongoDbConfig"));
builder.Services.AddSingleton<MongoDBContext>();
builder.Services.AddCardDependencies();
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

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