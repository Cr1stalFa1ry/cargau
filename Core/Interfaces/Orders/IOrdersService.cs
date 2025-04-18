using Core.Models;

namespace Core.Interfaces.Orders;

public interface IOrdersService
{
    Task<List<Order>> Get();
    Task<Order> GetById(Guid id);
    Task<Guid> Add(string client, Guid carId, string status);
    Task<bool> Update(Guid id, string client, Guid carId, string status);
    Task<bool> Delete(Guid id);
}
