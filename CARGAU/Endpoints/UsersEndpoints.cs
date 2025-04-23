using API.Dto.User;
using Core.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        //var group = app.MapGroup("user");

        app.MapPost("register", Register);
        app.MapPost("login", Login);

        return app;
    }

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
}
