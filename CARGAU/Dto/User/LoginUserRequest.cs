using System.ComponentModel.DataAnnotations;

namespace API.Dto.User;

public record class LoginUserRequest
(
    [Required(ErrorMessage = "Укажите электронную почту")]
    [EmailAddress(ErrorMessage = "Некорректный формат электронной почты")]
    string Email,

    [Required(ErrorMessage = "Укажите пароль")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен быть не менее 6 символов")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Пароль должен содержать как минимум одну заглавную букву, одну строчную букву и одну цифру")]
    string Password
);
