using Core.Models;

namespace Core.Interfaces.Services;

public interface IServicesRepository
{
    Task<int> Add(Service service);
    Task Delete(int id);
    Task<List<Service>> Get();
    Task<Service> GetById(int id);
    Task<Service> GetByName(string name);
    Task Update(Service serviceUpdate);
}