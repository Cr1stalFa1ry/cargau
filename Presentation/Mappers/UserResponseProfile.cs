using AutoMapper;
using Core.Models;

namespace Presentation.Mappers;

public class UserResponseProfile : Profile
{
    public UserResponseProfile()
    {
        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
    }
}

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
