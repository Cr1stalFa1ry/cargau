using Core.Models;
using Core.Interfaces.Cars;
using db.Entities;
using db.Context;
using Microsoft.EntityFrameworkCore;

namespace db.Repositories;

public class CarsRepository : ICarsRepository
{
    private readonly TuningContext _dbContext;

    public CarsRepository(TuningContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Car car)
    {
        var carEntity = new CarEntity
        {
            Id = car.Id,
            Brand = car.Brand,
            Model = car.Model,
            Price = car.Price,
            YearRelease = car.YearRelease,
            OwnerId = car.OwnerId
        };


        await _dbContext.Cars.AddAsync(carEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Car>> Get()
    {
        var carEntities =  await _dbContext.Cars
            .AsNoTracking()
            .ToListAsync();

        var cars = carEntities
            .Select(c => Car.Create(c.Id, c.Brand, c.Model, c.YearRelease, c.OwnerId, c.Price))
            .ToList();

        return cars;
    }

    public async Task<Car?> GetById(Guid id)
    {
        var car = await _dbContext.Cars
            .AsNoTracking()
            .FirstOrDefaultAsync(car => car.Id == id);

        if (car == null)
        {
            throw new ArgumentException("cat not found");
        }
        // нужно обработать ситуацию когда метод
        // FirstOrDefaultAsync вернет дефолтное значение сущности,
        // так как он не смог найти найти ее по id
        return Car.Create(car.Id, car.Brand, car.Model, car.YearRelease, car.OwnerId, car.Price);
    }

    public async Task Update(Car updateCar, Guid id)
    {
        var owner = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id == updateCar.OwnerId);

        if (owner == null)
        {
            throw new ArgumentException("owner not found");
        }

        await _dbContext.Cars
            .Where(car => car.Id == id)
            .ExecuteUpdateAsync(car => car
                .SetProperty(car => car.Price, updateCar.Price)
                .SetProperty(car => car.OwnerId, updateCar.OwnerId)
            );
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
