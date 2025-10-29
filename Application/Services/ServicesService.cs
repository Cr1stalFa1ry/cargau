using Core.Interfaces.Services;
using Core.Models;
using Core.Enum;

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

    public async Task<Service> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<Service> GetByName(string name)
    {
        return await _repository.GetByName(name);
    }

    public async Task<int> Add(string name, decimal price, string summary, TypeTuning type)
    {
        var newService = new Service(name, price, summary, type);

        return await _repository.Add(newService);
    }

    public async Task Update(int id, string name, decimal price, string summary, TypeTuning type = TypeTuning.TechTuning)
    {
        var updateService = new Service(name, price, summary, type, id);

        await _repository.Update(updateService);
    }

    public async Task Delete(int id)
    {
        await _repository.Delete(id);
    }
}
