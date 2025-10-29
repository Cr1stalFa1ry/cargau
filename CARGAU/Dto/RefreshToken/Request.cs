using System.ComponentModel.DataAnnotations;

namespace API.Dto.RefreshToken;

public record class Request
(
    [Required] 
    string? RefreshToken
);
