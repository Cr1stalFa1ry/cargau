using Core.Interfaces.Services;
using API.Dto.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class ServicesEndpoints
{
    public static IEndpointRouteBuilder MapServicesEnpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("services");

        group.MapGet("/", Get);
        group.MapGet("/{id:int}", GetById);
        group.MapGet("/{name}", GetByName);

        group.MapPost("/", Post);

        group.MapPut("/{id}", Put);

        group.MapDelete("/{id}", Delete);

        return group;
    }

    private static async Task<IResult> Get([FromServices] IServicesService service)
    {
        var services = await service.Get();

        return Results.Ok(services);
    }

    private static async Task<IResult> GetById(int id, [FromServices] IServicesService service)
    {
        var serviceById = await service.GetById(id);

        return Results.Ok(serviceById);
    }

    private static async Task<IResult> GetByName(
        [FromBody] string name, 
        [FromServices] IServicesService service)
    {
        var serviceByName = await service.GetByName(name);

        return Results.Ok(serviceByName);
    }

    private static async Task<IResult> Post(
        [FromBody] CreateService createService, 
        [FromServices] IServicesService service)
    {
        var id = await service.Add(
            createService.Name,
            createService.Price,
            createService.Summary,
            createService.TypeTuning
        );

        return Results.Created($"services/{id}", id);
    }

    private static async Task<IResult> Put(
        int id, 
        [FromBody] UpdateService updateService, 
        [FromServices] IServicesService service)
    {
        await service.Update(
            id,
            updateService.Name,
            updateService.Price,
            updateService.Summary,
            updateService.TypeTuning
        );
        
        return Results.NoContent();
    }

    private static async Task<IResult> Delete(int id, [FromServices] IServicesService service)
    {
        await service.Delete(id);

        return Results.NoContent();
    }
}
