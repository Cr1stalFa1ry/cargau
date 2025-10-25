using System.ComponentModel.DataAnnotations;
using Core.Enum;

namespace API.Dto.User;

public record class UpdateProfileRequest
(
    [Required]
    [StringLength(50, MinimumLength = 3)]
    string NewUserName,

    [Required] 
    [EmailAddress(ErrorMessage = "Invalid email address")]
    string NewEmail,

    [Required] 
    Roles Role
);
