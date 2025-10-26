using Core.Enum;
using Core.Models;

public class Order
{
    public Order(
        Guid id,
        Guid clientId,
        Guid carId,
        OrderStatus status,
        DateTime createdAt = default,
        decimal totalPrice = 0,
        List<Service>? selectedServices = default)
    {
        Id = id;
        ClientId = clientId;
        CarId = carId;
        Status = status;
        CreatedAt = createdAt;
        TotalPrice = totalPrice;
        SelectedServices = selectedServices;
    }

    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid CarId { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalPrice { get; set; }
    public List<Service>? SelectedServices { get; set; }
}