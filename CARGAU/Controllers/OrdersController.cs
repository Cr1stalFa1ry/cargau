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

    [Authorize]
    [HttpGet("get")]
    public async Task<IResult> GetOrders()
    {
        var orders = await _ordersService.Get();
        return Results.Ok(orders);
    }

    [Authorize]
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

    [Authorize]
    [HttpPut("update/{id}")]
    public async Task<IResult> UpdateOrder(Guid id, [FromBody] UpdateOrder updateOrder)
    {
        return await _ordersService.Update(id, updateOrder.ClientId, updateOrder.CarId, updateOrder.Status)
            ? Results.NoContent() : Results.NotFound("Order is not found");
    }

    [Authorize]
    [HttpDelete("delete/{id}")]
    public async Task<IResult> DeleteOrderById(Guid id)
    {
        return await _ordersService.Delete(id)
            ? Results.NoContent() : Results.NotFound("Order is not found");
    }

    [Authorize]
    [HttpPost("add-service")]
    public async Task<IResult> AddServiceToOrder([FromQuery] int serviceId, [FromQuery] Guid orderId)
    {
        await _ordersService.AddService(serviceId, orderId);
        return Results.NoContent();
    }
}
