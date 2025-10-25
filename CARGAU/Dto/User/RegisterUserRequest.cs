using Core.Enum;
using System.ComponentModel.DataAnnotations;

namespace API.Dto.User;

public record class RegisterUserRequest
(
    [Required]
    [StringLength(50, MinimumLength = 3)]
    string UserName,

    [Required]
    [EmailAddress]
    string Email,

    [Required]
    [StringLength(100, MinimumLength = 6)]
    string Password,

    [Required]
    Roles Role
);
