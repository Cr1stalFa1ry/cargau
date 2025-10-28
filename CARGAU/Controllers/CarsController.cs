using Core.Interfaces.Cars;
using API.Dto.Car;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API.Filters;

namespace API.Controllers;

[ApiController]
[Route("/cars")]
public class CarsController : ControllerBase
{
    private readonly ICarsService _carsService;
    public CarsController(ICarsService carsService)
    {
        _carsService = carsService;
    }

    [HttpGet("get-cars")]
    //[Authorize(Roles = "Admin")]
    public async Task<IResult> GetCars()
    {
        var cars = await _carsService.Get();
        return Results.Ok(cars);
    }

    [HttpGet("get-cars/{id}")]
    //[Authorize(Policy = "AdminOnly")]
    [ResponseHeader("Filter-Cars", "Cars")] // атрибут фильтра по добавлению заголовка в ответ
    [TypeFilter<LoggingFilter>(Arguments = ["Получение машины по id"])] // атрибут фильтра логирования, 
    // тут используется атрибут TypeFilter т.к. в конструктор LoggingFilter нужно передать параметр ILogger -> через DI
    public async Task<IResult> GetCarById(Guid id)
    {
        var car = await _carsService.GetById(id);

        return car != null ? Results.Ok(car) : Results.NotFound("car is not found");
    }

    [HttpGet("get-services-by-car/{carId}")]
    public async Task<IResult> GetServicesByCarId(Guid carId)
    {
        var services = await _carsService.GetServicesByCarId(carId);

        return Results.Ok(services);
    }

    [HttpGet("search")]
    public IActionResult SearchCar(string query, double price = 1)
    {
        return Ok($"Ищем: {query}, цена: {price}");
    }

    [HttpPost("add-car")]
    public async Task<IResult> AddCar([FromBody] CreateCarRequest request)
    {
        var id = await _carsService
            .Add(
                request.Brand,
                request.Model,
                request.OwnerId,
                request.YearRelease,
                request.Price);

        return Results.Created($"cars/{id}", id);
    }

    [HttpPatch("car/{carId}/update/price")]
    public async Task<IResult> UpdatePriceCar(Guid carId, [FromBody] UpdatePriceCarRequest request)
    {
        await _carsService.UpdatePrice(carId, request.Price);

        return Results.NoContent();
    }

    [HttpPatch("car/{carId}/owner/change")]
    public async Task<IResult> ChangeOwnerCar(Guid carId, [FromBody] UpdateOwnerCarRequest request)
    {
        await _carsService.ChangeOwner(carId, request.OwnerId);

        return Results.NoContent();
    }

    [HttpDelete("delete/{id}")]
    [Authorize]
    public async Task<IResult> DeleteCarById(Guid id)
    {
        await _carsService.DeleteById(id);

        return Results.NoContent();
    }
}
