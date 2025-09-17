using System;

namespace db.Entities;

public class RefreshTokenEntity
{
    public Guid Id { get; set; }
    public string Token { get; set; } = String.Empty;
    public Guid UserId { get; set; }
    public UserEntity? User { get; set; }
    public DateTime ExpiresOnUtc { get; set; }
}
