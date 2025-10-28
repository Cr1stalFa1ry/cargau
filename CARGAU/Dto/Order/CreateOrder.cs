using System.ComponentModel.DataAnnotations;
using Core.Models;

namespace API.Dto.Order;

public record class CreateOrder
(
    [Required(ErrorMessage = "Укажите идентификатор пользователя")]
    Guid ClientId,

    [Required(ErrorMessage = "Укажите идентификатор услуги")] 
    Guid CarId
);