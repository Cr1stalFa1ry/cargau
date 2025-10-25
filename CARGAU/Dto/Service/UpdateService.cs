using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Enum;

namespace API.Dto.Service;

public record class UpdateService
(
    string Name,
    [Required] decimal Price,
    string Summary,
    TypeTuning TypeTuning
);