using Core.Interfaces.Cars;
using Core.Models;

namespace Application.Services;

public class CarsService : ICarsService
{
    private readonly ICarsRepository _carRepository;
    public CarsService(ICarsRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<List<Car>> Get()
    {
        var cars = await _carRepository.Get();

        return cars;
    }

    public async Task<Car> GetById(Guid id)
    {
        return await _carRepository.GetById(id) ?? throw new ArgumentException("not found car");
    }

    public async Task<Guid> Add(string brand, string model, Guid ownerId, string yearRelease, decimal price)
    {
        var car = Car.Create(Guid.NewGuid(), brand, model, yearRelease, ownerId, price);

        await _carRepository.Add(car);

        return car.Id;
    }

    public async Task Update(Guid id, Guid ownerId, decimal price)
    {
        var carUpdate = Car.Create(ownerId, price);

        await _carRepository.Update(carUpdate, id);
    }

    public async Task<bool> DeleteById(Guid id)
    {
        return await _carRepository.Delete(id);
    }
}
