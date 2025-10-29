using Core.Enum;
using Core.Models;

namespace Core.Interfaces.Services;

public interface IServicesService
{
    Task<int> Add(string name, decimal price, string summary, TypeTuning type);
    Task Delete(int id);
    Task<List<Service>> Get();
    Task<Service> GetById(int id);
    Task<Service> GetByName(string name);
    Task Update(int id, string name, decimal price, string summary, TypeTuning type);
}
