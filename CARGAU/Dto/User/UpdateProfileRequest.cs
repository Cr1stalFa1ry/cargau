using System.ComponentModel.DataAnnotations;
using Core.Enum;

namespace API.Dto.User;

public record class UpdateProfileRequest
(
    [Required(ErrorMessage = "Укажите имя пользователя")]
    [StringLength(50, ErrorMessage = "Недопустимая длина имени пользователя")]
    string NewUserName,

    [Required(ErrorMessage = "Укажите электронную почту")]
    [EmailAddress(ErrorMessage = "Некорректный формат электронной почты")]
    string NewEmail,

    [Required(ErrorMessage = "Укажите роль пользователя")]
    [Range(1, 2, ErrorMessage = "Недопустимая роль пользователя")]
    Roles Role
);
