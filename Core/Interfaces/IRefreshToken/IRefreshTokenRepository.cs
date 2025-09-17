using Core.Models;

namespace Core.Interfaces.IRefreshToken;

public interface IRefreshTokenRepository
{
    Task AddToken(RefreshToken token);
    Task<bool> DeleteToken(Guid userId);
    
}
