using System.ComponentModel.DataAnnotations;
using Core.Enum;

namespace API.Dto.User;

public record class UpdateProfileRequest
(
    [Required] string NewUserName,
    [Required] string NewEmail,
    [Required] Roles Role
);
