using Core.Interfaces.Cars;
using API.Dto.Car;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class CarEndpoints
{
    public static IEndpointRouteBuilder MapCarEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("cars").RequireAuthorization();

        //group.WithTags("Cars");

        group.MapGet("/", Get);
        group.MapGet("/{id}", GetById);

        group.MapPost("/", Add);

        group.MapPut("/{id}", Update);

        group.MapDelete("/{id}", DeleteById);

        return group;
    }

    private static async Task<IResult> Get([FromServices] ICarsService carService)
    {
        var cars = await carService.Get();

        return Results.Ok(cars);
    }

    private static async Task<IResult> GetById(Guid id, [FromServices] ICarsService carsService)
    {
        var car = await carsService.GetById(id);

        return car != null ? Results.Ok(car) : Results.NotFound("car is not found");
    }

    private static async Task<IResult> Add(
        [FromBody] CreateCar car, 
        [FromServices] ICarsService carService)
    {
        var id = await carService.Add(car.Brand, car.Model, car.OwnerId, car.YearRelease, car.Price);

        return Results.Created($"cars/{id}", id);
    }

    private static async Task<IResult> Update(
        Guid id, 
        [FromBody] UpdateCar updateCar, 
        [FromServices] ICarsService carService)
    {
        await carService.Update(id, updateCar.OwnerId, updateCar.Price);

        return Results.NoContent(); 
    }

    private static async Task<IResult> DeleteById(Guid id, [FromServices] ICarsService carService)
    {
        return await carService.DeleteById(id)
            ? Results.NoContent() : Results.NotFound("Car is not found");
    }
}
