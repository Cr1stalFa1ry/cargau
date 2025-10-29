using Core.Enum;
using Core.Models;

namespace Core.Interfaces.Orders;

public interface IOrdersRepository
{
    Task<List<Order>> Get();
    Task<Order> GetById(Guid id);
    Task<List<Service>> GetServicesByOrderId(Guid orderId);
    Task Add(Order order);
    Task AddServicesToOrder(List<int> serviceIds, Guid orderId);
    Task<bool> UpdateStatus(Guid id, OrderStatus status);
    Task DeleteServices(List<int> serviceIds, Guid orderId);
    Task<bool> Delete(Guid id);
}
