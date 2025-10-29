using Core.Enum;
using System.ComponentModel.DataAnnotations;

namespace API.Dto.User;

public record class RegisterUserRequest
(
    [Required(ErrorMessage = "Укажите имя пользователя")]
    [StringLength(50, ErrorMessage = "Недопустимая длина имени пользователя")]
    string UserName,

    [Required(ErrorMessage = "Укажите электронную почту")]
    [EmailAddress(ErrorMessage = "Некорректный формат электронной почты")]
    string Email,

    [Required(ErrorMessage = "Укажите пароль")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен быть не менее 6 символов")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Пароль должен содержать как минимум одну заглавную букву, одну строчную букву и одну цифру")]
    string Password,

    [Required]
    [Range(1, 2, ErrorMessage = "Недопустимая роль пользователя\n1 - Клиент\n2 - Администратор")]
    Roles Role
);
