using System.ComponentModel.DataAnnotations;
using Core.Models;

namespace API.Dto.Order;

public record class CreateOrder
(
    [Required] string Client,
    [Required] Guid CarId ,
    //List<Service> SelectedServices,
    [Required] string Status
);