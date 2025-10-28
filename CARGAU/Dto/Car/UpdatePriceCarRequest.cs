using System.ComponentModel.DataAnnotations;

namespace API.Dto.Car;

public record class UpdatePriceCarRequest
(
    [Required(ErrorMessage = "Укажите цену")]
    [Range(0, 100_000_000, ErrorMessage = "Цена не должна быть отрицательной и не должна превышать 100 миллионов")]
    decimal Price
);
