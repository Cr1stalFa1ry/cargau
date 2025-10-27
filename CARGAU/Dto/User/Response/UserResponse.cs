namespace API.Dto.User.Response;

/// <summary>
/// Ответ с информацией о пользователе
/// </summary>
public record class UserResponse
(
    Guid Id,
    string UserName,
    string Email,
    string Role
);
