using System.ComponentModel.DataAnnotations;

namespace API.Dto.Car;

public record class UpdateCar
(
    decimal Price,
    string Owner
);
