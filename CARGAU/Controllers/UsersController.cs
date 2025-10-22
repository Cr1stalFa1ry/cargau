using API.Dto.User;
using Core.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;
    
    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet("get-current-user")]
    public async Task<IResult> GetCurrentUser()
    {
        var user = await _usersService.GetCurrentUser();

        return user != null ? Results.Ok(user) : Results.NotFound("user is not found");
    }

    [HttpPost("register")]
    // эти атрибуты можно опустить, они для документации
    [ProducesResponseType(StatusCodes.Status200OK)] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Register([FromBody] RegisterUserRequest request)
    {
        var (token, refreshToken) = await _usersService.Register(request.UserName, request.Email, request.Password, request.Role); 

        Response.Cookies.Append("cargau-cookies", token, new CookieOptions()
        {
            Expires = DateTimeOffset.UtcNow.AddMinutes(5)
        });

        return Ok(refreshToken);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginUserRequest request)
    {
        var (token, refreshToken) = await _usersService.Login(request.Email, request.Password);

        Response.Cookies.Append("cargau-cookies", token, new CookieOptions()
        {
            Expires = DateTimeOffset.UtcNow.AddMinutes(5)
        });

        return Ok(refreshToken);
    }

    [Authorize]
    [HttpPut("profile")]
    public async Task<ActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null || !Guid.TryParse(userId, out var userGuid))
            return Unauthorized();

        await _usersService.UpdateProfile(userGuid, request.NewUserName, request.NewEmail, request.Role);
        return Ok();
    }
}