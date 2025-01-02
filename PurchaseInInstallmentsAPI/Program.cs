using Data.Context;
using Data.Repositories;
using Domain.Base.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<EntityFramework>(dboptions =>
{
    dboptions.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConfig"));
});

builder.Services.AddScoped(typeof(IBaseRepositorie<>), typeof(BaseEntitieFrameworkRepositorie<>));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();