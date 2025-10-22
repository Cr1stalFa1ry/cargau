using API.Dto.RefreshToken;
using Core.Interfaces.Users;
using Core.Enum;
using db.Entities;
using db.Context;
using Microsoft.EntityFrameworkCore;
using Core.Models;
using Core.Interfaces.IRefreshToken;
using Presentation.Mappers.user;


namespace API.refreshToken;

public sealed class LoggingWithRefreshToken(
    TuningContext dbContext,
    IRefreshTokenProvider provider,
    IJwtProvider jwtProvider,
    IRefreshTokenRepository refreshTokenRepository)
{
    public async Task<Response> Handle(string? RefreshToken)
    {
        RefreshTokenEntity? refreshToken = await dbContext.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token.Equals(RefreshToken));

        if (refreshToken is null || refreshToken.ExpiresOnUtc <= DateTime.UtcNow)
            throw new Exception("Refresh token больше не действителен");

        var accessToken = jwtProvider.GenerateToken(ToUserModel(refreshToken.User)); // передаем пользователя и получаем новый токен доступа

        RefreshToken? rt = provider.GenerateRefreshToken(refreshToken.User!.ToModel());

        await refreshTokenRepository.AddToken(rt); // добавляем токен в базу данных

        return new Response(rt.Token, accessToken);
    }

    public User ToUserModel(UserEntity? user)
    {
        return new User(user!.Id, user.UserName, user.PasswordHash, user.Email);
    }

}
