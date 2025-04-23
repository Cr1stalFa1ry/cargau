using Core.Models;

namespace Core.Interfaces
{
    public interface ICarsRepository
    {
        Task Add(Car car);
        Task<List<Car>> Get();
        Task<Car?> GetById(Guid id);
        Task Update(Car updateCar, Guid id);
        Task<bool> Delete(Guid id);
    }
}