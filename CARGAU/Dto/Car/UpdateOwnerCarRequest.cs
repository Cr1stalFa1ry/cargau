using System.ComponentModel.DataAnnotations;

namespace API.Dto.Car;

public record class UpdateOwnerCarRequest
(
    [Required(ErrorMessage = "Укажите владельца автомобиля")]
    Guid OwnerId
);
