using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Enum;

namespace API.Dto.Service;

public record class UpdateService
(
    [Required(ErrorMessage = "Укажите название услуги")]
    [StringLength(50, ErrorMessage = "Недопустимая длина названия услуги")]
    string Name,

    [Required(ErrorMessage = "Укажите цену услуги")] 
    [Range(1, 10_000_000, ErrorMessage = "Надо другую цену, брат")]
    decimal Price,

    [Required(ErrorMessage = "Укажите описание услуги")] 
    [StringLength(500, ErrorMessage = "Недопустимая длина описания услуги")]
    string Summary,

    [Required(ErrorMessage = "Укажите тип тюнинга автомобиля")] 
    [Range(1, 4, ErrorMessage = "Недопустимый тип тюнинга автомобиля")]
    TypeTuning TypeTuning
);