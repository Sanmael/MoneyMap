using PurchaseAPI.EndPoints;
using PurchaseAPI.Dependencies;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Business.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EntityFramework>(dboptions =>
{
    dboptions.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConfig"));
});

builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection("MongoDbConfig"));

builder.Services.AddSingleton<MongoDBContext>();

builder.AddJwtAuthorize();

builder.Services.AddStackExchangeRedisCache(config =>
{
    config.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddPurchaseDependencies();
// Add services to the container.
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection(); 
app.MapPurchaseEndPoints();
app.Run();