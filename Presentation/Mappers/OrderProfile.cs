using AutoMapper;
using db.Entities;

namespace Presentation.Mappers;

public class OrderProfile : Profile
{
    public OrderProfile() 
    {
        CreateMap<OrderEntity, Order>()
            .ForMember(dest => dest.SelectedServices, opt => opt.MapFrom(src => src.SelectedServices));

        CreateMap<Order, OrderEntity>()
            .ForMember(dest => dest.SelectedServices, opt => opt.MapFrom(src => src.SelectedServices));
    }
}
