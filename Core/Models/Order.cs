using System.ComponentModel;

public class Order
{
    public Order(
        Guid id, 
        Guid clientId, 
        Guid carId, 
        OrderStatus status = default, 
        DateTime createdAt = default)
    {
        Id = id;
        ClientId = clientId;
        CarId = carId;
        Status = status;
        CreatedAt = createdAt;
    }

    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid CarId { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalPrice { get; set; }
}

public enum OrderStatus
{
    [Description("")]
    New = 1,

    [Description("")]
    Confirmed,

    [Description("")]
    InProgress,

    [Description("")]
    WaitingForPayment,

    [Description("")]
    Completed,

    [Description("")]
    Cancelled,

    [Description("")]
    OnHold,
    Default
}