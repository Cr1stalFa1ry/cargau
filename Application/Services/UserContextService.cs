using Core.Interfaces.Users;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetCurrentUserEmail()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        return user!.FindFirst(ClaimTypes.Email)!.Value;
    }

    public Guid? GetCurrentUserId()
    {
        var user = _httpContextAccessor.HttpContext?.User; // предварительно вытаскиваем пользователя

        if (user == null) 
            throw new ArgumentException("user not found"); // исключение, информирующее об отсутствии пользователя

        var userIdClaim = user.FindFirst("userId")?.Value;
        return userIdClaim != null ? Guid.Parse(userIdClaim) : null;
    }
}

