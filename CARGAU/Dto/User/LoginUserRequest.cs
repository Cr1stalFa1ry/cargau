using System.ComponentModel.DataAnnotations;

namespace API.Dto.User;

public record class LoginUserRequest
(
    [Required] string Email,
    [Required] string Password
);
