
using Core.Models;

namespace Application.Services
{
    public interface ICarsService
    {
        Task<Guid> Add(string brand, string model, string owner, decimal price);
        Task<bool> DeleteById(Guid id);
        Task<List<Car>> Get();
        Task<Car> GetById(Guid id);
        Task Update(Guid id, string owner, decimal price);
    }
}