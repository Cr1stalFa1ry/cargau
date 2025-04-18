using System;

namespace Core.Models;

public class Order
{
    public Order(Guid id, string client, Guid carId, string status, DateTime createdAt)
    {
        Id = id;
        Client = client;
        CarId = carId;
        Status = status;
        CreatedAt = createdAt;
    }
    public Guid Id { get; set; }
    public string? Client { get; set; }
    public Guid CarId { get; set; }
    public List<Service> SelectedServices { get; set; } = [];
    public string? Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public static Order Create(Guid id, string client, Guid carId, string status, DateTime createdAt)
    {
        return new Order(id, client, carId, status, createdAt);
    }
}
