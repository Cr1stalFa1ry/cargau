using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("user");



        return group;
    }

    //private static async Task<IResult> Get([FromServices] IUsersService userService)
    //{
    //    return;
    //}

    //private static async Task<IResult> GetById([FromServices] IUsersService userService)
    //{
    //    return;
    //}
}
