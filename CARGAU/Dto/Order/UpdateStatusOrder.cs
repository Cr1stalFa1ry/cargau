using Core.Enum;
using System.ComponentModel.DataAnnotations;

namespace API.Dto.Order;

public record class UpdateStatusOrder
(
    [Required(ErrorMessage = "Укажите идентификатор заказа")]
    [Range(1, 8, ErrorMessage = "Недопустимый статус заказа")] 
    OrderStatus Status
);