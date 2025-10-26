using Core.Enum;
using System.ComponentModel.DataAnnotations;

namespace API.Dto.Order;

public record class UpdateStatusOrder
(
    [Required] OrderStatus Status
);