using Core.Models;

namespace Core.Interfaces.Services;

public interface IServicesRepository
{
    Task<Guid> Add(Service service);
    Task Delete(Guid id);
    Task<List<Service>> Get();
    Task<Service> GetById(Guid id);
    Task<Service> GetByName(string name);
    Task Update(Service serviceUpdate);
}