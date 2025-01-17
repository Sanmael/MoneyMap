using Data.Context;
using Microsoft.EntityFrameworkCore;
using UserAPI.EndPoints;
using Business.Extensions;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddStackExchangeRedisCache(config =>
{
    config.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.AddJwtAuthorize();

builder.Services.AddDbContext<EntityFramework>(dboptions =>
{
    dboptions.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConfig"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseMiddleware<RequestLoggingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapperUserEndPoint();
app.MapperAuthenticationEndPoint();
app.Run();