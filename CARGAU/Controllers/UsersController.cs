using API.Dto.User;
using Core.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace API.Controllers;

[ApiController]
[Route("/user")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;
    private readonly IHttpContextAccessor _httpContext;
    
    public UsersController(IUsersService usersService, IHttpContextAccessor httpContext)
    {
        _usersService = usersService;
        _httpContext = httpContext;
    }

    [HttpGet("get-all-users")]
    [Authorize(Roles = "Admin")]
    public async Task<IResult> GetUsers()
    {
        var users = await _usersService.GetUsersAsync();
        return Results.Ok(users);
    }

    [HttpGet("get-current-user")]
    [Authorize]
    public async Task<IResult> GetCurrentUser()
    {
        var user = await _usersService.GetCurrentUser();

        return user != null ? Results.Ok(user) : Results.NotFound("user is not found");
    }

    [HttpPost("register")]
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

    [HttpPut("update-profile")]
    [Authorize]
    public async Task<ActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
    {
        // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        // if (userId == null || !Guid.TryParse(userId, out var userGuid))
        //     return Unauthorized();
        var currentUser = _httpContext.HttpContext?.User;
        if (currentUser == null)
            throw new ArgumentNullException("Пользователь не найден, нужно войти в профиль или зарегистрироваться");

        var userId = currentUser?.FindFirst("userId")!.Value;
        if (userId == null || !Guid.TryParse(userId, out var userGuid))
            return Unauthorized("user or userId not found");

        await _usersService.UpdateProfile(userGuid, request.NewUserName, request.NewEmail, request.Role);
        return Ok();
    }
}