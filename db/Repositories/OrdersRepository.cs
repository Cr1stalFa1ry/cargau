using Core.Models;
using Core.Interfaces.Orders;
using db.Context;
using db.Entities;
using Microsoft.AspNetCore.WebUtilities;
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
            .Select(o => Order.Create(o.Id, o.Client, o.CarId, o.Status, o.Date))
            .ToList();

        return orders;
    }

    public async Task<Order> GetById(Guid id)
    {
        var order = await _dbContext.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(order => order.Id == id);

        // надо сделать autoMapping, по крайней мере попробовать что это такое, и с чем это едят
        return Order.Create(order.Id, order.Client, order.CarId, order.Status, order.Date);
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

        var orderEntity = new OrderEntity
        {
            Id = order.Id,
            Client = order.Client,
            CarId = order.CarId,
            Date = order.CreatedAt, // можно убрать свойство у модели, и просто присваивать каждый раз при создании сущности Date = DateTime.UtcNow
            Status = order.Status
            //SelectedServices = selectedServices
        };

        await _dbContext.Orders.AddAsync(orderEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> Update(Guid id, Order updateOrder)
    {
        // тут надо подумать, что можно менять, а что оставить не изменяемым
        var result = await _dbContext.Orders
            .Where(order => order.Id == id)
            .ExecuteUpdateAsync(order => order
                .SetProperty(o => o.Status, updateOrder.Status));

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
