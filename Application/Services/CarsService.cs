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

    public async Task<List<Service>> GetServicesByCarId(Guid carId)
    {
        return await _carRepository.GetServicesByCarId(carId);
    }

    public async Task<Guid> Add(string brand, string model, Guid ownerId, string yearRelease, decimal price)
    {
        var car = Car.Create(Guid.NewGuid(), brand, model, yearRelease, ownerId, price);

        await _carRepository.Add(car);

        return car.Id;
    }

    public async Task UpdatePrice(Guid carId, decimal newPrice)
    {
        var carUpdate = Car.Create(carId, newPrice);

        await _carRepository.UpdatePrice(carUpdate);
    }

    public async Task ChangeOwner(Guid carId, Guid ownerId)
    {
        var carUpdate = Car.Create(carId, ownerId);

        await _carRepository.UpdateOwner(carUpdate);
    }

    public async Task<bool> DeleteById(Guid id)
    {
        return await _carRepository.Delete(id);
    }
}
