using Core.Interfaces.Cars;
using Core.Interfaces.Orders;
using Core.Interfaces.Users;
using Core.Interfaces.Services;
using Core.Interfaces.IRefreshToken;
using Application.Services;
using db.Context;
using db.Repositories;
using API.refreshToken;
using API.Jwt;
using API.Hash;
using API.Endpoints;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FluentValidation;
using Presentation.Mappers;

var builder = WebApplication.CreateBuilder(args);

// DI регистрация сервисов в контейнере
builder.Services.AddControllers();
    
builder.Services.AddEndpointsApiExplorer();

// Добавление валидации FluentValidation
builder.Services
    .AddFluentValidationAutoValidation()       // автоматическая серверная валидация
    .AddFluentValidationClientsideAdapters();  // поддержка для клиента (если нужно)

builder.Services
    .AddValidatorsFromAssemblyContaining<Program>(); // регистрирует все валидаторы в сборке

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<UserProfile>();
    cfg.AddProfile<ServiceProfile>();
    cfg.AddProfile<OrderProfile>();
    cfg.AddProfile<CarProfile>();
    cfg.AddProfile<RoleProfile>();
    cfg.AddProfile<UserResponseProfile>();
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddLogging();

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection(nameof(JwtOptions)));

// добавление в сервисы 
builder.Services.AddApiAuthentication(builder.Configuration);

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

// использование swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting(); // можно и не вызывать т.к. по умолчанию вначале pipeline вызывается

app.UseAuthentication(); // кто вы?
app.UseAuthorization(); // какие права у вас есть?

//app.RequestCulture(); // кастомный middleware для установки культуры из query параметра

app.MapControllers();

app.MapRefreshTokenEndpoints();

app.Run();