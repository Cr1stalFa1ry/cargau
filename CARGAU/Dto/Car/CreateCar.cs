using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace API.Dto.Car;

public record class CreateCarRequest
(
    [Required(ErrorMessage = "Укажите марку автомобиля")]
    [StringLength(50, ErrorMessage = "Недопустимая длина текста")]
    string Brand,

    [Required(ErrorMessage = "Укажите модель автомобиля")]
    [StringLength(30, ErrorMessage = "Недопустимая длина текста")]
    string Model,

    [Required(ErrorMessage = "Укажите цену")]
    [Range(0, 100_000_000, ErrorMessage = "Недопустимая цена")]
    decimal Price,

    [Required(ErrorMessage = "Укажите год выпуска")]
    [RegularExpression(@"^\d{4}$", ErrorMessage = "Год выпуска должен быть в формате YYYY")]
    string YearRelease,

    [Required(ErrorMessage = "Укажите валидный идентификатор владельца")]
    Guid? OwnerId
);