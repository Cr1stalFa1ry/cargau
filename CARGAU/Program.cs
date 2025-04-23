using Core.Interfaces;
using Core.Interfaces.Orders;
using Core.Interfaces.Users;
using db.Context;
using db.Repositories;
using API.Endpoints;
using API.Jwt;
using API.Hash;
using Application.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DI
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddScoped<ICarsRepository, CarsRepository>();
builder.Services.AddScoped<ICarsService, CarsService>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();

//builder.Services.AddAutoMapper(typeof(OrderProfile));

builder.Services.AddDbContext<TuningContext>(
    options => 
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(TuningContext)));
    }
);

var app = builder.Build();

app.MapCarEndpoints();
app.MapOrdersEndpoints();
app.MapUsersEndpoints();

app.Run();

