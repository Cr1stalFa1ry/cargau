using Core.Interfaces.Cars;
using Core.Interfaces.Orders;
using Core.Interfaces.Users;
using Core.Interfaces.Services;
using Core.Interfaces.IRefreshToken;
using Application.Services;
using db.Context;
using db.Repositories;
using db.Options;
using API.refreshToken;
using API.Jwt;
using API.Hash;
using API.Endpoints;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

//builder.Services.AddLogging();

// DI
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

// пока не используется
builder.Services.Configure<AuthorizationOptions>(builder.Configuration.GetSection(nameof(AuthorizationOptions)));

// добавление в сервисы 
builder.Services.AddApiAuthentication(builder.Configuration);

// пока не используется
builder.Services.AddScoped(provider =>
{
    var options = new AuthorizationOptions();
    builder.Configuration.GetSection(nameof(AuthorizationOptions)).Bind(options);
    return options;
});

builder.Services.AddScoped<LoggingWithRefreshToken>();
builder.Services.AddScoped<IRefreshTokenProvider, RefreshTokenProvider>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddScoped<ICarsRepository, CarsRepository>();
builder.Services.AddScoped<ICarsService, CarsService>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<IServicesRepository, ServicesRepository>();
builder.Services.AddScoped<IServicesService, ServicesService>();


builder.Services.AddDbContext<TuningContext>(
    options => 
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(TuningContext)));
    }
);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));

    options.AddPolicy("UserOnly", policy =>
        policy.RequireRole("User"));
});

// swagger
//builder.Services.AddEndpointsApiExplorer(); // собирает всю информацию о эндпоинтах, проще говоря сканер
builder.Services.AddSwaggerGen(); // генератор интерактивной генерации по API

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

//app.UseHttpsRedirection();

// использование swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting(); // можно и не вызывать т.к. по умолчанию вначале pipeline оно вызывается

app.UseAuthentication(); // кто вы?
app.UseAuthorization(); // какие права у вас есть?

//app.RequestCulture(); // кастомный middleware для установки культуры из query параметра

app.MapControllers();

app.MapRefreshTokenEndpoints();
// Использование endpoints
// app.MapCarEndpoints();
// app.MapOrdersEndpoints();
// app.MapUsersEndpoints();
// app.MapServicesEnpoints();

app.Run();