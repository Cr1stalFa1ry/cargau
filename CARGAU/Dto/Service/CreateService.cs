using System.ComponentModel.DataAnnotations;

namespace API.Dto.Service;

public record class CreateService
(
    [Required] string Name,
    [Required] decimal Price,
    [Required] string Summary
);
