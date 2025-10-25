using Core.Models;

namespace Core.Interfaces.Orders;

public interface IOrdersService
{
    Task<List<Order>> Get();
    Task<Order> GetById(Guid id);
    Task<Guid> Add(Guid clientId, Guid carId);
    Task<bool> Update(Guid id, Guid clientId, Guid carId, OrderStatus status);
    Task<bool> Delete(Guid id);
    Task AddService(int serviceId, Guid orderId);
}
