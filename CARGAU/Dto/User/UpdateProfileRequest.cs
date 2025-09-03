using System.ComponentModel.DataAnnotations;

namespace API.Dto.User;

public record class UpdateProfileRequest
(
    [Required] string NewUserName,
    [Required] string NewEmail
);
