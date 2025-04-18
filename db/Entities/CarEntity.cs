using System;

namespace db.Entities;

public class CarEntity
{
    public Guid Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime DateRelease { get; set; }
    public string Owner { get; set; } = string.Empty;
}
