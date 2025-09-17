namespace Core.Models;

public class RefreshToken
{
    public RefreshToken(Guid id, string token, Guid userId, User user, DateTime expiresOnUtc)
    {
        Id = id;
        Token = token;
        UserId = userId;
        User = user;
        ExpiresOnUtc = expiresOnUtc;
    }
    public Guid Id { get; set; }
    public string Token { get; set; } = String.Empty;
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public DateTime ExpiresOnUtc { get; set; }
}
