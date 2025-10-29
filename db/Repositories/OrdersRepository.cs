using Core.Enum;
using Core.Models;
using Core.Interfaces.Orders;
using db.Context;
using db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace db.Repositories;

public class OrdersRepository : IOrdersRepository
{
    private readonly TuningContext _dbContext;
    private readonly ILogger<OrdersRepository> _logger;
    private readonly IMapper _mapper;
    public OrdersRepository(TuningContext dbContext, ILogger<OrdersRepository> logger, IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<List<Order>> Get()
    {
        var orderEntities = await _dbContext.Orders
            .AsNoTracking()
            .ToListAsync();

        var orders = orderEntities
            .Select(order => _mapper.Map<Order>(order))
            .ToList();

        return orders;
    }

    public async Task<Order> GetById(Guid id)
    {
        var order = await _dbContext.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(order => order.Id == id)
            ?? throw new ArgumentException("order not found");

        return _mapper.Map<Order>(order);
    }

    public async Task<List<Service>> GetServicesByOrderId(Guid orderId)
    {
        var order = await _dbContext.Orders
            .Include(order => order.SelectedServices)
            .FirstOrDefaultAsync(order => order.Id == orderId)
            ?? throw new ArgumentException("order not found");

        var selectedServices = order.SelectedServices
            .Select(service => _mapper.Map<Service>(service))
            .ToList();

        return selectedServices;
    }

    public async Task Add(Order order)
    {
        var client = await _dbContext.Users
            .FirstOrDefaultAsync(user => user.Id == order.ClientId)
            ?? throw new ArgumentException("client not found");

        var car = await _dbContext.Cars
            .FirstOrDefaultAsync(car => car.Id == order.CarId)
            ?? throw new ArgumentException("car not found");

        var orderEntity = _mapper.Map<OrderEntity>(order);
        orderEntity.Client = client;
        orderEntity.Car = car;

        await _dbContext.Orders.AddAsync(orderEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> UpdateStatus(Guid id, OrderStatus status)
    {
        var result = await _dbContext.Orders
            .Where(order => order.Id == id)
            .ExecuteUpdateAsync(order => order
                .SetProperty(o => o.Status, status)
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

    public async Task AddServicesToOrder(List<int> serviceIds, Guid orderId)
    {
        try
        {
            // получаем заказ, включая список услуг, чтобы можно было добавить новые услуги
            var order = await _dbContext.Orders
                .Include(order => order.SelectedServices)
                .FirstOrDefaultAsync(order => order.Id == orderId)
                ?? throw new ArgumentNullException($"Order with id {orderId} doesn't exist");

            // получаем уже существующие услуги в заказе
            var existingServiceIds = order.SelectedServices.Select(service => service.Id).ToList();

            // выбираем услуги для добавления только те, которых нету уже в выбранных и есть в списке serviceIds
            var serviceIdsToAdd = await _dbContext.Services
                .Where(service => serviceIds.Contains(service.Id) && !existingServiceIds.Contains(service.Id))
                .ToListAsync()
                ?? throw new ArgumentNullException();

            // если список пустой, то уже все услуги добавлены
            if (!serviceIdsToAdd.Any())
                return;

            // добавляем все новые услуги в заказ
            foreach (var service in serviceIdsToAdd)
            {
                if (!order.SelectedServices.Any(os => os.Id == service.Id))
                {
                    order.SelectedServices.Add(service);
                }
            }

            // считаем общую стоимость всех выбранных услуг
            order.TotalPrice = order.SelectedServices.Sum(service => service.Price);

            await _dbContext.SaveChangesAsync();
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogWarning(ex, $"Validation error adding services to order {orderId}");
            throw;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError($"Database error adding services to order {orderId}");
            throw new InvalidOperationException("Error saving services to order", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unexpected error adding services to order {orderId}");
            throw;
        }
    }

    public async Task DeleteServices(List<int> serviceIds, Guid orderId)
    {
        try
        {
            var order = await _dbContext.Orders
                .Include(order => order.SelectedServices)
                .FirstOrDefaultAsync(order => order.Id == orderId)
                ?? throw new ArgumentNullException($"order with id {orderId} doesn't exist");

            foreach (var serviceId in serviceIds)
            {
                order.SelectedServices.RemoveAll(s => s.Id == serviceId);
            }

            order.TotalPrice = order.SelectedServices.Sum(service => service.Price);

            await _dbContext.SaveChangesAsync();
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogWarning(ex, $"Не получилось совершить какое то действие");
            throw;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError($"Database error delete services to order {orderId}");
            throw new InvalidOperationException("Error delete services to order", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unexpected error adding services to order {orderId}");
            throw;
        }
    }
}
