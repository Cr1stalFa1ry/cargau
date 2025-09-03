using Core.Models;

namespace Core.Interfaces.Services;

public interface IServicesService
{
    Task<Guid> Add(string name, decimal price, string summary);
    Task Delete(Guid id);
    Task<List<Service>> Get();
    Task<Service> GetById(Guid id);
    Task<Service> GetByName(string name);
    Task Update(Guid id, string name, decimal price, string summary);
}
