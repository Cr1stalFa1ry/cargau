using Core.Interfaces.Services;
using API.Dto.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ServicesController
{
    private readonly IServicesService _servicesService;

    public ServicesController(IServicesService servicesService)
    {
        _servicesService = servicesService;
    }

    [HttpGet("get")]
    [Authorize]
    public async Task<IResult> GetServices()
    {
        var services = await _servicesService.Get();
        return Results.Ok(services);
    }

    [HttpGet("get/{id}")]
    [Authorize]
    public async Task<IResult> GetServiceById(Guid id)
    {
        var service = await _servicesService.GetById(id);

        return service != null ? Results.Ok(service) : Results.NotFound("service is not found");
    }

    [HttpPost("add")]
    [Authorize]
    public async Task<IResult> AddService([FromBody] CreateService service)
    {
        var id = await _servicesService.Add(service.Name, service.Price, service.Summary);

        return Results.Created($"services/{id}", id);
    }

    [HttpPut("update/{id}")]
    [Authorize]
    public async Task<IResult> UpdateService(Guid id, [FromBody] UpdateService updateService)
    {
        await _servicesService.Update(id, updateService.Name, updateService.Price, updateService.Summary);

        return Results.NoContent();
    }
}
