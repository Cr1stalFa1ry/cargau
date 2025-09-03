using Core.Models;
using Core.Interfaces.Services;
using db.Context;
using db.Entities;
using Microsoft.EntityFrameworkCore;

namespace db.Repositories;

public class ServicesRepository : IServicesRepository
{
    private readonly TuningContext _dbContext;

    public ServicesRepository(TuningContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Service>> Get()
    {
        var listEntities = await _dbContext.Services
            .AsNoTracking()
            .ToListAsync();

        var listServices = listEntities
            .Select(s => Service.Create(s.Id, s.Name, s.Price, s.Summary))
            .ToList();

        return listServices;
    }

    public async Task<Service> GetById(Guid id)
    {
        var serviceById = await _dbContext.Services
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);

        if (serviceById == null)
        {
            throw new ArgumentNullException("service not found");
        }

        return Service.Create(serviceById.Id, serviceById.Name, serviceById.Price, serviceById.Summary);
    }

    public async Task<Service> GetByName(string name)
    {
        var serviceById = await _dbContext.Services
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Name == name);

        if (serviceById == null)
        {
            throw new ArgumentNullException("service not found");
        }

        return Service.Create(serviceById.Id, serviceById.Name, serviceById.Price, serviceById.Summary);
    }

    public async Task<Guid> Add(Service service)
    {
        var serviceEntity = new ServiceEntity
        {
            Id = service.Id,
            Name = service.NameService,
            Price = service.Price,
            Summary = service.Summary
        };

        await _dbContext.Services.AddAsync(serviceEntity);
        await _dbContext.SaveChangesAsync();

        return serviceEntity.Id;
    }

    public async Task Update(Service serviceUpdate)
    {
        var res = await _dbContext.Services
            .Where(s => s.Id == serviceUpdate.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(s => s.Name, serviceUpdate.NameService)
                .SetProperty(s => s.Price, serviceUpdate.Price)
                .SetProperty(s => s.Summary, serviceUpdate.Summary)
            );

        if (res == 0)
        {
            throw new ArgumentNullException("service not found");
        }
    }

    public async Task Delete(Guid id)
    {
        var res = await _dbContext.Services
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync();

        if (res == 0)
        {
            throw new ArgumentNullException("service not found");
        }
    }
}
