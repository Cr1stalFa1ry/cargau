using Core.Models;

namespace db.Entities;

public class OrderEntity
{
    public Guid Id { get; set; }
    public UserEntity? Client { get; set; }
    public Guid ClientId { get; set; }
    public CarEntity? Car { get; set; }
    public Guid CarId { get; set; }
    public List<ServiceEntity> SelectedServices { get; set; } = [];
    //public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
