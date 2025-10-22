using Core.Enum;
using System.ComponentModel.DataAnnotations;

namespace API.Dto.User;

public record class RegisterUserRequest
(
    [Required] string UserName,
    [Required] string Email,
    [Required] string Password,
    [Required] Roles Role
);
