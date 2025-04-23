using System.ComponentModel.DataAnnotations;

namespace API.Dto.Car;

public record class CreateCar
(
    [Required] string Brand,
    [Required] string Model,
    [Required] decimal Price,
    [Required] string YearRelease,
    [Required] Guid OwnerId
);