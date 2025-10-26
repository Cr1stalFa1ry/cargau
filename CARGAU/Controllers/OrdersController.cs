using Core.Interfaces.Orders;
using API.Dto.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[ApiController]
[Route("/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService _ordersService;

    public OrdersController(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }

    [HttpGet("get")]
    public async Task<IResult> GetOrders()
    {
        var orders = await _ordersService.Get();
        return Results.Ok(orders);
    }

    [HttpGet("get/{id}")]
    public async Task<IResult> GetOrderById(Guid id)
    {
        var orderById = await _ordersService.GetById(id);

        return orderById != null ? Results.Ok(orderById) : Results.NotFound("order is not found");
    }

    [Authorize]
    [HttpPost("add")]
    public async Task<IResult> CreateOrder([FromBody] CreateOrder newOrder)
    {
        var id = await _ordersService.Add(newOrder.ClientId, newOrder.CarId);

        return Results.Created($"orders/{id}", id);
    }

    [HttpPatch("update-status/{id}")]
    public async Task<IResult> UpdateStatusOrder(Guid id, [FromQuery] UpdateStatusOrder updateOrder)
    {
        return await _ordersService.UpdateStatus(id, updateOrder.Status)
            ? Results.NoContent() : Results.NotFound("order is not found");
    }

    [Authorize]
    [HttpDelete("delete/{id}")]
    public async Task<IResult> DeleteOrderById(Guid id)
    {
        return await _ordersService.Delete(id)
            ? Results.NoContent() : Results.NotFound("Order is not found");
    }

    [HttpPost("add-services")]
    public async Task<IResult> AddServicesToOrder([FromBody] List<int> listServices, [FromQuery] Guid orderId)
    {
        await _ordersService.AddServices(listServices, orderId);
        return Results.NoContent();
    }

    [HttpPut("delete-services")]
    public async Task<IResult> DeleteServicesFromOrder([FromBody] List<int> listServices, [FromQuery] Guid orderId)
    {
        await _ordersService.DeleteServices(listServices, orderId);
        return Results.NoContent();
    }
}
