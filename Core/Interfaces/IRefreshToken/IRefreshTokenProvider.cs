using Core.Models;

namespace Core.Interfaces.Users;

public interface IRefreshTokenProvider
{
    RefreshToken GenerateRefreshToken(User user);
    string GenerateToken();
    Task<bool> RevokeRefreshToken(Guid userId);
}
