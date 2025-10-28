
using Core.Models;

namespace Core.Interfaces.Cars
{
    public interface ICarsService
    {
        Task<List<Car>> Get();
        Task<Car> GetById(Guid id);
        Task<List<Service>> GetServicesByCarId(Guid carId);
        Task<Guid> Add(string brand, string model, Guid? ownerId, string yearRelease, decimal price);
        Task UpdatePrice(Guid carId, decimal newPrice);
        Task ChangeOwner(Guid carId, Guid ownerId);
        Task<bool> DeleteById(Guid id);
    }
}