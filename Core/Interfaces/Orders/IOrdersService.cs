using Core.Enum;

namespace Core.Interfaces.Orders;

public interface IOrdersService
{
    Task<List<Order>> Get();
    Task<Order> GetById(Guid id);
    Task<Guid> Add(Guid clientId, Guid carId);
    Task<bool> UpdateStatus(Guid id, OrderStatus status);
    Task<bool> Delete(Guid id);
    Task AddServices(List<int> listOfServices, Guid orderId);
    Task DeleteServices(List<int> listServices, Guid orderId);
}
