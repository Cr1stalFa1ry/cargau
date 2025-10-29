using AutoMapper;
using Core.Models;
using db.Entities;

namespace Presentation.Mappers;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        // авто мапперу нужно чтобы в модели были просто свойства без ctor, а если есть, 
        // то просто через эти замечательные методы ForCtorParam() указываем что брать в параметр конструктора
        CreateMap<ServiceEntity, Service>()
            .ForCtorParam("nameService", opt => opt.MapFrom(src => src.Name))
            .ForCtorParam("price", opt => opt.MapFrom(src => src.Price))
            .ForCtorParam("summary", opt => opt.MapFrom(src => src.Summary))
            .ForCtorParam("type", opt => opt.MapFrom(src => src.TypeTuning))
            .ForCtorParam("id", opt => opt.MapFrom(src => src.Id));

        CreateMap<Service, ServiceEntity>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
            .ForMember(dest => dest.TypeTuning, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }
}
