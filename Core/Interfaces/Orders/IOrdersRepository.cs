using Core.Models;

namespace Core.Interfaces.Orders;

public interface IOrdersRepository
{
    Task<List<Order>> Get();
    Task<Order> GetById(Guid id);
    Task Add(Order order);
    Task<bool> Update(Guid id, Order updateOrder);
    Task<bool> Delete(Guid id);
}
