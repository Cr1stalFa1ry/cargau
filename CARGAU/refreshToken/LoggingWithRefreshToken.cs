using API.Dto.RefreshToken;
using Core.Interfaces.Users;
using db.Entities;
using db.Context;
using Microsoft.EntityFrameworkCore;
using Core.Models;
using Core.Interfaces.IRefreshToken;
using AutoMapper;


namespace API.refreshToken;

public sealed class LoggingWithRefreshToken(
    TuningContext dbContext,
    IRefreshTokenProvider provider,
    IJwtProvider jwtProvider,
    IRefreshTokenRepository refreshTokenRepository,
    IMapper mapper)
{
    public async Task<Response> Handle(string? RefreshToken)
    {
        RefreshTokenEntity? refreshToken = await dbContext.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token.Equals(RefreshToken));

        if (refreshToken is null || refreshToken.ExpiresOnUtc <= DateTime.UtcNow)
            throw new ArgumentNullException("Refresh token больше не действителен");

        // передаем пользователя и получаем новый токен доступа
        var accessToken = jwtProvider.GenerateToken(mapper.Map<User>(refreshToken.User)); 

        RefreshToken? rt = provider.GenerateRefreshToken(mapper.Map<User>(refreshToken.User));

         // добавляем токен в базу данных
        await refreshTokenRepository.AddToken(rt);

        return new Response(rt.Token, accessToken);
    }
}
