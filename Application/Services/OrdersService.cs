using Core.Enum;
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

    public async Task<List<Service>> GetServicesByOrderId(Guid orderId)
    {
        return await _ordersRepository.GetServicesByOrderId(orderId);
    }

    public async Task<Guid> Add(Guid clientId, Guid carId)
    {
        var order = new Order(Guid.NewGuid(), clientId, carId, OrderStatus.New, DateTime.UtcNow);

        await _ordersRepository.Add(order);

        return order.Id;
    }

    public async Task AddServices(List<int> listServices, Guid orderId)
    {
        await _ordersRepository.AddServicesToOrder(listServices, orderId);
    }

    public async Task<bool> UpdateStatus(Guid id, OrderStatus status)
    {
        return await _ordersRepository.UpdateStatus(id, status);
    }

    public async Task<bool> Delete(Guid id)
    {
        return await _ordersRepository.Delete(id);
    }

    public async Task DeleteServices(List<int> listServices, Guid orderId)
    {
        await _ordersRepository.DeleteServices(listServices, orderId);
    }
}
