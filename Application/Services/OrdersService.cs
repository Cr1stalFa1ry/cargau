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

    public async Task<Guid> Add(Guid clientId, Guid carId)
    {
        var order = Order.Create(clientId, carId);

        await _ordersRepository.Add(order);

        return order.Id;
    }

    public async Task<bool> Update(Guid id, Guid clientId, Guid carId, OrderStatus status)
    {
        var tempOrder = Order.CreateUpdateOrder(id, clientId, carId, status);

        return await _ordersRepository.Update(tempOrder);
    }

    public async Task<bool> Delete(Guid id)
    {
        return await _ordersRepository.Delete(id);
    }
}
