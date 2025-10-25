using AutoMapper;
using Core.Enum;
using db.Entities;

namespace Presentation.Mappers;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<RoleEntity, Roles>()
            .ConvertUsing(src => (Roles)src.Id);

        CreateMap<Roles, RoleEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int)src))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ToString()));
    }
}
