using Core.Interfaces;
using Application.Services;
using db.Context;
using db.Repositories;
using API.Endpoints;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces.Orders;

var builder = WebApplication.CreateBuilder(args);

// DI
builder.Services.AddScoped<ICarsRepository, CarsRepository>();
builder.Services.AddScoped<ICarsService, CarsService>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IOrdersService, OrdersService>();

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

app.Run();

