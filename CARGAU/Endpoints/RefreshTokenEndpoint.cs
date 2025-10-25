using API.Dto.RefreshToken;
using API.refreshToken;
using Core.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class RefreshTokenEndpoint
{
    public static IEndpointRouteBuilder MapRefreshTokenEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("user-rt");
        group.MapPost("/refresh-token", Login);
        group.MapDelete("/{id}/refresh-tokens", Delete).RequireAuthorization();
        //group.MapGet("/get-currtne-user", GetCurrentUser);

        return group;
    }

    private static async Task<Response> Login(
        [FromBody] Request request,
        [FromServices] LoggingWithRefreshToken useCase,
        HttpContext http)
    {
        var response = await useCase.Handle(request.RefreshToken);

        http.Response.Cookies.Append("cargau-cookies", response.AccessToken!, new CookieOptions() 
        {
            Expires = DateTimeOffset.UtcNow.AddMinutes(5)
        });
        
        return response;
    }

    private static async Task<IResult> Delete(Guid id, [FromServices] IRefreshTokenProvider useCase)
    {   
        bool success = await useCase.RevokeRefreshToken(id);

        return success ? Results.NoContent() : Results.BadRequest();   
    }
}