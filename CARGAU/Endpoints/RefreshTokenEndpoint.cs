using API.Dto.RefreshToken;
using API.refreshToken;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class RefreshTokenEndpoint
{
    public static IEndpointRouteBuilder MapRefreshTokenEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("users");
        group.MapPost("/refresh-token", Login);
        group.MapDelete("/{id:guid}/refresh-tokens", Delete).RequireAuthorization();

        return group;
    }

    private static async Task<Response> Login(
        [FromBody] Request request,
        [FromServices] LoggingWithRefreshToken useCase)
    {
        return await useCase.Handle(request.RefreshToken);
    }

    private static async Task<IResult> Delete(Guid id, [FromServices] RefreshTokenProvider useCase)
    {   
        bool success = await useCase.RevokeRefreshToken(id);

        return success ? Results.NoContent() : Results.BadRequest();   
    }
}