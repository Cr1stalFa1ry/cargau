using System.ComponentModel.DataAnnotations;

namespace API.Dto.RefreshToken;

public record class Response
(
    [Required] string? RefreshToken,
    [Required] string? AccessToken
);
