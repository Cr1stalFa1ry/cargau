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

    // метод, применяющийся при создании заказа
    public static Order Create(Guid clientId, Guid carId)
    {
        return new Order(Guid.NewGuid(), clientId, carId, OrderStatus.New, DateTime.UtcNow);
    }

    // метод, использующийся для создания вспомогательной модели для возврата клиенту(надо использовать dto)
    public static Order Create(
        Guid id, 
        Guid clientId, 
        Guid carId, 
        OrderStatus status, 
        DateTime createdAt)
    {
        return new Order(id, clientId, carId, status, createdAt);
    }

    public static Order CreateUpdateOrder(Guid id, Guid clientId, Guid carId, OrderStatus updateStatus)
    {
        return new Order(id, clientId, carId, updateStatus);
    }
}

public enum OrderStatus
{
    [Description("����� �����")]
    New,

    [Description("�����������")]
    Confirmed,

    [Description("� ������")]
    InProgress,

    [Description("������� ������")]
    WaitingForPayment,

    [Description("��������")]
    Completed,

    [Description("�������")]
    Cancelled,

    [Description("�������������")]
    OnHold,
    Default
}