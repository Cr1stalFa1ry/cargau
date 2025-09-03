using System.Security.Claims;
using API.Dto.User;
using Core.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        //var group = app.MapGroup("user");
        //app.MapGet("get-users", GetUsers);

        app.MapPost("register", Register);
        app.MapPost("login", Login);

        app.MapPut("update-profile", UpdateProfile);
            //.RequireAuthorization();

        return app;
    }

    // [Authorize(Roles = "admin")]
    // private static async Task<IResult> GetUsers([FromServices] IUsersService usersService)
    // {
    //     var users = await usersService.GetUsersAsync();

    //     return Results.Ok(users);
    // }

    private static async Task<IResult> Register(
        [FromBody] RegisterUserRequest request,
        [FromServices] IUsersService usersService)
    {
        await usersService.Register(request.UserName, request.Email, request.Password);

        return Results.Ok();
    }

    private static async Task<IResult> Login(
        [FromBody] LoginUserRequest request, 
        [FromServices] IUsersService usersService,
        HttpContext context)
    {
        // проверить email и пароль
        // создать токен, который подтверждает, что мы вошли в систему
        // сохранить токен в куки

        // все три пункта, описанные выше будут реализованы в .Login()
        var token = await usersService.Login(request.Email, request.Password);

        // добавляем токен в куки, в первом параметре передаем название токена, 
        // и поэтому его лучше не называть явно, чтобы его не смогли легко перехватить
        context.Response.Cookies.Append("cargau-cookies", token);

        return Results.Ok(token);
    }

    [Authorize]
    private static async Task<IResult> UpdateProfile(
        [FromBody] UpdateProfileRequest request,
        [FromServices] IUsersService usersService,
        HttpContext context)
    {
        var userId = context.User.FindFirst("userId")?.Value;
        if (userId == null)
            return Results.Unauthorized();

        await usersService.UpdateProfile(Guid.Parse(userId), request.NewUserName, request.NewEmail);
        return Results.NoContent();
    }
}
