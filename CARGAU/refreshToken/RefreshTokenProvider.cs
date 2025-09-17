using System.Security.Claims;
using System.Security.Cryptography;
using Core.Interfaces.IRefreshToken;
using Core.Interfaces.Users;
using Core.Models;

namespace API.refreshToken;

public class RefreshTokenProvider : IRefreshTokenProvider
{
    private readonly IRefreshTokenRepository _rtRep;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public RefreshTokenProvider(IRefreshTokenRepository rtRep, IHttpContextAccessor httpContextAccessor)
    {
        _rtRep = rtRep;
        _httpContextAccessor = httpContextAccessor;
    }

    public RefreshToken GenerateRefreshToken(User user)
    {
        return new RefreshToken(Guid.NewGuid(),
                                GenerateToken(),
                                user.Id,
                                user,
                                DateTime.UtcNow.AddDays(7));
    }

    public string GenerateToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }

    public async Task<bool> RevokeRefreshToken(Guid userId)
    {
        if (userId != GetCurrentUser())
        {
            throw new Exception("Oops, you cant do this");
        }

        var res = await _rtRep.DeleteToken(userId);

        return res;
    }

    public Guid? GetCurrentUser()
    {
        return Guid.TryParse(
            _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier), 
            out Guid parsed)
            ? parsed : null;
    }
}
