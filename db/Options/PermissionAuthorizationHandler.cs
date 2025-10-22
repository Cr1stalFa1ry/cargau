using Microsoft.AspNetCore.Authorization;
using db.Options.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Core.Interfaces.Permissions;

namespace db.Options;

// IAuthorizationHandler - нужен для обратки всех всех requirement в системе
public class PermissionAuthorizationHandler :
    AuthorizationHandler<PermissionRequirement> // реализует IAuthorizationHanlder используем этот абстрактный класс, т.к. реализуем именно один PermissionRequirement
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionRequirement requirement)
    {
        // вытаскиваем id пользователя
        var userId = context.User.Claims.FirstOrDefault(
            c => c.Type == CustomClaims.UserId);

        // проверяем что все получилось
        if (userId == null || !Guid.TryParse(userId.Value, out var id))
        {
            return;
        }

        // нужно вытащить наш сервис PermissionService из DI, но так как он будет 
        // singleton, то нужно использовать IServiceScopeFactory, создаем scope 
        using var scope = _serviceScopeFactory.CreateScope();
        var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

        // вытаскиваем все разрешения
        var permissions = await permissionService.GetPermissionAsync(id);

        // пересекается хоть одно разрешение с тем, что в requirement
        if (permissions.Intersect(requirement.Permissions).Any())
        {
            // вызываем контекст requirement
            context.Succeed(requirement);
        }
    }
}

