using System.ComponentModel.DataAnnotations;
using Core.Enum;

namespace API.Dto.Service;

public record class CreateService
(
    [Required] string Name,
    [Required] decimal Price,
    [Required] string Summary,
    [Required] TypeTuning TypeTuning
);
