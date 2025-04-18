using Core.Models;
using Core.Interfaces.Orders;

namespace Application.Services;

public class OrdersService : IOrdersService
{
    private readonly IOrdersRepository _ordersRepository;

    public OrdersService(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task<List<Order>> Get()
    {
        var ordersFromRep = await _ordersRepository.Get();

        return ordersFromRep;
    }

    public async Task<Order> GetById(Guid id)
    {
        return await _ordersRepository.GetById(id);
    }

    public async Task<Guid> Add(string client, Guid carId, string status)
    {
        var order = Order.Create(Guid.NewGuid(), client, carId, status, DateTime.UtcNow);

        await _ordersRepository.Add(order);

        return order.Id;
    }

    public async Task<bool> Update(Guid id, string client, Guid carId, string status)
    {
        var tempOrder = Order.Create(id, client, carId, status, DateTime.UtcNow);

        return await _ordersRepository.Update(id, tempOrder);
    }

    public async Task<bool> Delete(Guid id)
    {
        return await _ordersRepository.Delete(id);
    }
}
