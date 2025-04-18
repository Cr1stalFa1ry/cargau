using Core.Interfaces.Orders;
using API.Dto.Order;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class OrdersEndpoints
{
    public static IEndpointRouteBuilder MapOrdersEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("orders");

        group.MapGet("/", Get);
        group.MapGet("/{id}", GetById);

        group.MapPost("/", Add);

        group.MapPut("/", Update);

        group.MapDelete("/", Delete);

        return group;
    }

    private static async Task<IResult> Get([FromServices] IOrdersService ordersService)
    {
        var orders = await ordersService.Get();

        return Results.Ok(orders);
    }

    private static async Task<IResult> GetById(Guid id, [FromServices] IOrdersService ordersService)
    {
        var orderById = await ordersService.GetById(id);

        return Results.Ok(orderById);
    }

    private static async Task<IResult> Add(
        [FromBody] CreateOrder newOrder, 
        [FromServices] IOrdersService ordersService)
    {
        var id = await ordersService.Add(newOrder.Client, newOrder.CarId, newOrder.Status);

        return Results.Created($"/orders/{id}", id);
    }

    private static async Task<IResult> Update(
        Guid id,
        [FromBody] UpdateOrder updateOrder,
        [FromServices] IOrdersService ordersService)
    {
        return await ordersService.Update(id, updateOrder.Client, updateOrder.CarId, updateOrder.Status)
            ? Results.NoContent() : Results.NotFound("Order is not found");
    }

    private static async Task<IResult> Delete(Guid id, [FromServices] IOrdersService ordersService)
    {
        return await ordersService.Delete(id) ? Results.NoContent() : Results.NotFound("Order is not found");
    }
}
