using Core.Models;

namespace Core.Interfaces.Cars
{
    public interface ICarsRepository
    {
        Task Add(Car car);
        Task<List<Car>> Get();
        Task<Car?> GetById(Guid id);
        Task<List<Service>> GetServicesByCarId(Guid carId);
        Task UpdateOwner(Car updateCar);
        Task UpdatePrice(Car updateCar);
        Task<bool> Delete(Guid id);
    }
}