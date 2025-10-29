using Core.Models;
using Core.Interfaces.Cars;
using db.Entities;
using db.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace db.Repositories;

public class CarsRepository : ICarsRepository
{
    private readonly TuningContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<CarsRepository> _logger;

    public CarsRepository(TuningContext dbContext, IMapper mapper, ILogger<CarsRepository> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Add(Car car)
    {
        var carEntity = _mapper.Map<CarEntity>(car);

        await _dbContext.Cars.AddAsync(carEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Car>> Get()
    {
        var carEntities =  await _dbContext.Cars
            .AsNoTracking()
            .ToListAsync();

        var cars = carEntities
            .Select(car => _mapper.Map<Car>(car))
            .ToList();

        return cars;
    }

    public async Task<Car?> GetById(Guid id)
    {
        var car = await _dbContext.Cars
            .AsNoTracking()
            .FirstOrDefaultAsync(car => car.Id == id)
            ?? throw new ArgumentException("cat not found");

        return _mapper.Map<Car>(car);
    }

    public async Task<List<Service>> GetServicesByCarId(Guid carId)
    {
        try
        {
            // загружаем только услуги, без необходимости загрузки заказов
            var serviceEntities = await _dbContext.Orders
                .Include(order => order.SelectedServices)
                .Where(order => order.CarId == carId)
                .SelectMany(order => order.SelectedServices)
                .ToListAsync();
            
            if (serviceEntities.Count == 0)
            {
                return new List<Service>();
            }

            var mappedServices = serviceEntities
                .Select(service => _mapper.Map<Service>(service))
                .DistinctBy(service => service.Id)
                .ToList();

            return mappedServices;
        }
        catch(Exception)
        {
            _logger.LogError("Error retrieving services for car with ID {CarId}", carId);
            throw;
        }
    }

    public async Task UpdateOwner(Car updateCar)
    {
        try
        {
            var owner = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Id == updateCar.OwnerId)
                ?? throw new ArgumentException($"user with id: {updateCar.OwnerId}, not found");

            await _dbContext.Cars
                .Where(car => car.Id == updateCar.Id)
                .ExecuteUpdateAsync(car => car
                    .SetProperty(car => car.OwnerId, updateCar.OwnerId)
                );
        }
        catch (ArgumentException)
        {
        
        }
        catch (Exception)
        {
            
        }
    }

    public async Task UpdatePrice(Car updateCar)
    {
        try
        {
            await _dbContext.Cars
                .Where(car => car.Id == updateCar.Id)
                .ExecuteUpdateAsync(car => car
                    .SetProperty(car => car.Price, updateCar.Price)
                );
        }
        catch (ArgumentNullException)
        {

        }
        catch (Exception)
        {
            
        }
    }

    public async Task<bool> Delete(Guid id)
    {
        var result = await _dbContext.Cars
            .Where(car => car.Id == id)
            .ExecuteDeleteAsync();

        // при успешном удалении результат будет всегда > 0
        return result > 0;
    }
}
