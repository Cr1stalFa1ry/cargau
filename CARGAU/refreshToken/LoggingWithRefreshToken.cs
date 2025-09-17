using API.Dto.RefreshToken;
using Core.Interfaces.Users;
using db.Entities;
using db.Context;
using Microsoft.EntityFrameworkCore;
using Core.Models;


namespace API.refreshToken;

internal sealed class LoggingWithRefreshToken(
    TuningContext dbContext,
    IRefreshTokenProvider provider,
    IJwtProvider jwtProvider)
{
    public async Task<Response> Handle(string? RefreshToken)
    {
        RefreshTokenEntity? refreshToken = await dbContext.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token.Equals(RefreshToken));

        if (refreshToken is null)
            throw new Exception("Refresh token больше не действителен");

        var accessToken = jwtProvider.GenerateToken(ToUserModel(refreshToken.User)); // передаем пользователя и получаем новый токен доступа

        refreshToken.Token = provider.GenerateToken(); // создаем новый токен доступа
        refreshToken.ExpiresOnUtc = DateTime.UtcNow.AddDays(7); // добавляем время жизни токена

        return new Response()
        {
            RefreshToken = refreshToken.Token,
            AccessToken = accessToken
        };
    }

    public User ToUserModel(UserEntity? user)
    {
        return new User(user.Id, user.UserName, user.PasswordHash, user.Email);
    }

}
