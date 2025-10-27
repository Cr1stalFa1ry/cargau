using System.Security.Claims;
using System.Security.Cryptography;
using Core.Interfaces.IRefreshToken;
using Core.Interfaces.Users;
using Core.Models;
using db.Entities;

namespace API.refreshToken;

public class RefreshTokenProvider : IRefreshTokenProvider
{
    private readonly IRefreshTokenRepository _rtRep;
    private readonly IUserContextService _userContextService;
    public RefreshTokenProvider(
        IRefreshTokenRepository rtRep, 
        IUserContextService userContextService)
    {
        _rtRep = rtRep;
        _userContextService = userContextService;
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
        // другой пользователь не может удалить чужой токен
        if (userId != _userContextService.GetCurrentUserId())
        {
            throw new Exception("Oops, you cant do this");
        }

        var res = await _rtRep.DeleteToken(userId);

        return res;
    }
}
