using System.ComponentModel.DataAnnotations;
using Core.Models;

namespace API.Dto.Order;

public record class CreateOrder
(
    [Required] Guid ClientId,
    [Required] Guid CarId
);