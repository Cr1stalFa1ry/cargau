using Core.Enum;

namespace Core.Interfaces.Orders;

public interface IOrdersRepository
{
    Task<List<Order>> Get();
    Task<Order> GetById(Guid id);
    Task Add(Order order);
    Task<bool> UpdateStatus(Guid id, OrderStatus status);
    Task<bool> Delete(Guid id);
    Task AddServicesToOrder(List<int> serviceIds, Guid orderId);
    Task DeleteServices(List<int> serviceIds, Guid orderId);
}
