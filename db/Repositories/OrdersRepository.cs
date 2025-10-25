using Core.Interfaces.Orders;
using db.Context;
using db.Entities;
using Microsoft.EntityFrameworkCore;

namespace db.Repositories;

public class OrdersRepository : IOrdersRepository
{
    private readonly TuningContext _dbContext;
    public OrdersRepository(TuningContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Order>> Get()
    {
        var orderEntities = await _dbContext.Orders
            .AsNoTracking()
            .ToListAsync();

        var orders = orderEntities
            .Select(order => new Order(order.Id, order.ClientId, order.CarId, order.Status, order.CreatedAt))
            .ToList();

        return orders;
    }

    public async Task<Order> GetById(Guid id)
    {
        var order = await _dbContext.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(order => order.Id == id);

        if (order == null)
        {
            throw new ArgumentException("order not found");
        }

        // надо сделать autoMapping, по крайней мере попробовать что это такое
        return new Order(order.Id, order.ClientId, order.CarId, order.Status, order.CreatedAt);
    }

    public async Task Add(Order order)
    {
        var client = await _dbContext.Users
            .FirstOrDefaultAsync(user => user.Id == order.ClientId);
        
        if (client == null)
        {
            throw new ArgumentException("client not found");
        }

        var car = await _dbContext.Cars
            .FirstOrDefaultAsync(car => car.Id == order.CarId);

        if (car == null)
        {
            throw new ArgumentException("car not found");
        }

        var orderEntity = new OrderEntity
        {
            Id = order.Id,
            Client = client,
            ClientId = order.ClientId,
            Car = car,
            CarId = order.CarId,
            CreatedAt = order.CreatedAt, // можно убрать свойство у модели,
                                    // и просто присваивать каждый раз при создании сущности Date = DateTime.UtcNow
            Status = order.Status
            //SelectedServices = selectedServices
        };

        await _dbContext.Orders.AddAsync(orderEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> Update(Order updateOrder)
    {
        var client = await _dbContext.Users
           .FirstOrDefaultAsync(user => user.Id == updateOrder.ClientId);

        if (client == null)
        {
            throw new ArgumentException("client not found");
        }

        var car = await _dbContext.Cars
            .FirstOrDefaultAsync(car => car.Id == updateOrder.CarId);

        if (car == null)
        {
            throw new ArgumentException("car not found");
        }

        var result = await _dbContext.Orders
            .Where(order => order.Id == updateOrder.Id)
            .ExecuteUpdateAsync(order => order
                .SetProperty(o => o.Status, updateOrder.Status)
                .SetProperty(o => o.CarId, updateOrder.CarId)
                .SetProperty(o => o.ClientId, updateOrder.ClientId)
            );

        return result > 0;
    }

    public async Task<bool> Delete(Guid id)
    {
        var result = await _dbContext.Orders
            .Where(order => order.Id == id)
            .ExecuteDeleteAsync();

        return result > 0;
    }

    public async Task AddServicesToOrder(int serviceId, Guid orderId)
    {
        var order = await _dbContext.Orders
            .Include(order => order.SelectedServices)
            .FirstOrDefaultAsync(order => order.Id == orderId)
            ?? throw new ArgumentNullException("order doesnt exist");

        var service = await _dbContext.Services
            .AsNoTracking()
            .FirstOrDefaultAsync(service => service.Id == serviceId)
            ?? throw new ArgumentNullException("service doesnt exist");

        if (!order.SelectedServices.Any(os => os.Id == serviceId))
        {
            order.SelectedServices.Add(new ServiceEntity
            {
                Id = service.Id,
                Name = service.Name,
                Summary = service.Summary,
                Price = service.Price,
                TypeTuning = service.TypeTuning
            });
        }

        //order.TotalPrice = order.SelectedServices.Sum(service => service.Price);

        await _dbContext.SaveChangesAsync();
    }
}
