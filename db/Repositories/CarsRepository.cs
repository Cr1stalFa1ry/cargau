using Core.Models;
using Core.Interfaces;
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
            Owner = car.Owner,
            DateRelease = DateTime.UtcNow
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
            .Select(c => Car.Create(c.Id, c.Brand, c.Model, c.Owner, c.Price))
            .ToList();

        return cars;
    }

    public async Task<Car?> GetById(Guid id)
    {
        CarEntity? car = await _dbContext.Cars
            .AsNoTracking()
            .FirstOrDefaultAsync(car => car.Id == id);

        // нужно обработать ситуацию когда метод
        // FirstOrDefaultAsync вернет дефолтное значение сущности,
        // так как он не смог найти найти ее по id
        return Car.Create(car.Id, car.Brand, car.Model, car.Owner, car.Price);
    }

    public async Task Put(Car updateCar, Guid id)
    {
        await _dbContext.Cars
            .Where(car => car.Id == id)
            .ExecuteUpdateAsync(car => car
                .SetProperty(car => car.Price, updateCar.Price)
                .SetProperty(car => car.Owner, updateCar.Owner)
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
