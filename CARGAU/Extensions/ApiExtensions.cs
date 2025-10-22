using System.Text;
using Core.Enum;
using db.Options;
using API.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.Interfaces.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace API.Extensions;

public static class ApiExtensions
{
    public static void AddApiAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();

        // схемы (паттерны) определяют поведение middleware аутентификации
        // проверяет наличие токена в header 
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false, // издатель
                    ValidateAudience = false, // получатель
                    ValidateLifetime = true, // время жизни токена
                    ValidateIssuerSigningKey = true, // секретный ключ издателя
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions!.SecretKey)) // сам секретный ключ
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["cargau-cookies"];

                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Ошибка аутентификации: {context.Exception}");
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddScoped<IPermissionService, PermissionService>();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>(); // почему мы его ЗАРЕГАЛИ КАК SINGLETON???

        services.AddAuthorization();
    }

    // статический метод расширения для указания разрешения на каждый endpoint
    public static IEndpointConventionBuilder RequirePermissions<TBuilder>(
        this TBuilder builder, params Permissions[] permissions)
            where TBuilder : IEndpointConventionBuilder
    {
        return builder.RequireAuthorization(policy =>
            policy.AddRequirements(new PermissionRequirement(permissions)));
    }
}
