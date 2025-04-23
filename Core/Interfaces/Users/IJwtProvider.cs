using Core.Models;

namespace Core.Interfaces.Users;

public interface IJwtProvider
{
    public string GenerateToken(User user);
}