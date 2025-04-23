using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Dto.Order;

public record class UpdateOrder
(
    [Required] Guid ClientId,
    [Required] Guid CarId,
    [Required] OrderStatus Status
    //List<Service> SelectedServices
);