using System.ComponentModel.DataAnnotations;

namespace API.Dto.User;

public record class LoginUserRequest
(
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    string Email,

    [Required] 
    [StringLength(100, MinimumLength = 6)]
    string Password
);
