using System;

namespace db.Entities;

public class CarEntity
{
    public Guid Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string YearRelease { get; set; } = string.Empty;
    public UserEntity Owner { get; set; } = null!;
    public Guid OwnerId { get; set; }
    public List<OrderEntity> Orders { get; set; } = [];
}
