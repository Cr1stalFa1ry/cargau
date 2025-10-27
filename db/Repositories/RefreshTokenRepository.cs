using Core.Interfaces.IRefreshToken;
using Core.Models;
using db.Context;
using db.Entities;
using Microsoft.EntityFrameworkCore;

namespace db.Repositories;

public class RefreshTokenRepository(TuningContext dbContext) : IRefreshTokenRepository
{
    public async Task AddToken(RefreshToken token)
    {
        var rtEntity = new RefreshTokenEntity()
        {
            Id = token.Id,
            UserId = token.UserId,
            Token = token.Token,
            ExpiresOnUtc = token.ExpiresOnUtc
        };

        await dbContext.RefreshTokens.AddAsync(rtEntity);
        await dbContext.SaveChangesAsync();
    }


    public async Task<bool> DeleteToken(Guid userId)
    {
        var result = await dbContext.RefreshTokens
            .Where(r => r.UserId == userId)
            .ExecuteDeleteAsync();

        return result == 0 ? false : true;
    }
}
