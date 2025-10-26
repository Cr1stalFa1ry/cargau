using AutoMapper;
using Core.Models;
using Core.Enum;
using db.Entities;

namespace Presentation.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, User>()
            .ForCtorParam("role", opt => opt.MapFrom(src => (Roles)src.Role!.Id));
            // .ForMember(dest => dest.Role, opt => opt.MapFrom(src => (Roles) src.Role!.Id));
        // for member не сработал т.к. у меня сеттер приватный, поэтому использовал ForCtorParam, 
        // который напрямую мапит параметры конструктора
        CreateMap<User, UserEntity>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => new RoleEntity { Id = (int)src.Role }));
    }
}
