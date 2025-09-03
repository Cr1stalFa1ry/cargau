using Core.Interfaces.Services;
using Core.Models;

namespace Application.Services;

public class ServicesService : IServicesService
{
    private readonly IServicesRepository _repository;

    public ServicesService(IServicesRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Service>> Get()
    {
        return await _repository.Get();
    }

    public async Task<Service> GetById(Guid id)
    {
        return await _repository.GetById(id);
    }

    public async Task<Service> GetByName(string name)
    {
        return await _repository.GetByName(name);
    }

    public async Task<Guid> Add(string name, decimal price, string summary)
    {
        var newService = Service.Create(name, price, summary);

        return await _repository.Add(newService);
    }

    public async Task Update(Guid id, string name, decimal price, string summary)
    {
        var updateService = Service.Create(id, name, price, summary);

        await _repository.Update(updateService);
    }

    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);
    }
}
