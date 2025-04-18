using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Dto.Order;

public record class UpdateOrder
(
    string Client,
    Guid CarId,
    [Required] string Status
    //List<Service> SelectedServices
);