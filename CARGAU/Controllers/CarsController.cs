using Core.Interfaces.Cars;
using API.Dto.Car;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API.Filters;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class CarsController : ControllerBase
{
    private readonly ICarsService _carsService;
    public CarsController(ICarsService carsService)
    {
        _carsService = carsService;
    }

    [HttpGet("get")]
    [Authorize(Roles = "Admin")]
    public async Task<IResult> GetCars()
    {
        var cars = await _carsService.Get();
        return Results.Ok(cars);
    }

    [HttpGet("get/{id}")]
    [Authorize(Policy = "AdminOnly")]
    [ResponseHeader("Filter-Cars", "Cars")] // атрибут фильтра по добавлению заголовка в ответ
    [TypeFilter<LoggingFilter>(Arguments = ["Получение машины по id"])] // атрибут фильтра логирования, 
    // тут используется атрибут TypeFilter т.к. в конструктор LoggingFilter нужно передать параметр ILogger -> через DI
    public async Task<IResult> GetCarById(Guid id)
    {
        var car = await _carsService.GetById(id);

        return car != null ? Results.Ok(car) : Results.NotFound("car is not found");
    }

    [HttpGet("search")]
    public IActionResult SearchCar(string query, double price = 1)
    {
        return Ok($"Ищем: {query}, цена: {price}");
    }

    [HttpPost("add")]
    [Authorize]
    public async Task<IResult> AddCar([FromBody] CreateCar car)
    {
        var id = await _carsService.Add(car.Brand, car.Model, car.OwnerId, car.YearRelease, car.Price);

        return Results.Created($"cars/{id}", id);
    }

    [HttpPut("update/{id}")]
    [Authorize]
    public async Task<IResult> UpdateCar(Guid id, [FromBody] UpdateCar updateCar)
    {
        await _carsService.Update(id, updateCar.OwnerId, updateCar.Price);

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
