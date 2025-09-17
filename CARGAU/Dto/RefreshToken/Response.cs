using System.ComponentModel.DataAnnotations;

namespace API.Dto.RefreshToken;

public record class Response
{
    [Required] public string? RefreshToken;
    [Required] public string? AccessToken;
}
