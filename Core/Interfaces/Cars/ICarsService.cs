
using Core.Models;

namespace Core.Interfaces.Cars
{
    public interface ICarsService
    {
        Task<Guid> Add(string brand, string model, Guid ownerId, string yearRelease, decimal price);
        Task<List<Car>> Get();
        Task<Car> GetById(Guid id);
        Task Update(Guid id, Guid ownerId, decimal price);
        Task<bool> DeleteById(Guid id);
    }
}