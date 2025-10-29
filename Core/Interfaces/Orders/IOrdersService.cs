using Core.Enum;
using Core.Models;

namespace Core.Interfaces.Orders;

public interface IOrdersService
{
    Task<List<Order>> Get();
    Task<Order> GetById(Guid id);
    Task<List<Service>> GetServicesByOrderId(Guid orderId);
    Task<Guid> Add(Guid clientId, Guid carId);
    Task AddServices(List<int> listOfServices, Guid orderId);
    Task<bool> UpdateStatus(Guid id, OrderStatus status);
    Task<bool> Delete(Guid id);
    Task DeleteServices(List<int> listServices, Guid orderId);
}
