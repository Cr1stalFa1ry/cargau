using Core.Models;

namespace Core.Interfaces
{
    public interface ICarsRepository
    {
        Task Add(Car car);
        Task<bool> Delete(Guid id);
        Task<List<Car>> Get();
        Task<Car?> GetById(Guid id);
        Task Put(Car updateCar, Guid id);
    }
}