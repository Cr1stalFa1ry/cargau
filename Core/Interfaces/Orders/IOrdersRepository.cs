using Core.Models;

namespace Core.Interfaces.Orders;

public interface IOrdersRepository
{
    Task<List<Order>> Get();
    Task<Order> GetById(Guid id);
    Task Add(Order order);
    Task<bool> Update(Order updateOrder);
    Task<bool> Delete(Guid id);
    Task AddServicesToOrder(int serviceId, Guid orderId);
}
