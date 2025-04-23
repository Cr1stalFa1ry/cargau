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
            .Select(order => Order.Create(order.Id, order.ClientId, order.CarId, order.Status, order.Date))
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
        return Order.Create(order.Id, order.ClientId, order.CarId, order.Status, order.Date);
    }

    public async Task Add(Order order)
    {
        //var selectedServices = order.SelectedServices
        //    .Select(s => new ServiceEntity
        //    {
        //        Id = s.Id,
        //        NameService = s.NameService,
        //        Price = s.Price,
        //        Summary = s.Summary
        //    })
        //    .ToList();

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
            Date = order.CreatedAt, // можно убрать свойство у модели,
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
}
