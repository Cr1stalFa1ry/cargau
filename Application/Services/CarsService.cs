using Core.Interfaces;
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
        return await _carRepository.GetById(id);
    }

    public async Task<Guid> Add(string brand, string model, string owner, decimal price)
    {
        var car = Car.Create(Guid.NewGuid(), brand, model, owner, price);

        await _carRepository.Add(car);

        return car.Id;
    }

    public async Task Update(Guid id, string owner, decimal price)
    {
        var carUpdate = Car.Create(id, "", "", owner, price);

        await _carRepository.Put(carUpdate, id);
    }

    public async Task<bool> DeleteById(Guid id)
    {
        return await _carRepository.Delete(id);
    }
}
