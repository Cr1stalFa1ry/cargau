using AutoMapper;
using db.Entities;

namespace Presentation.Mappers;

public class OrderProfile : Profile
{
    public OrderProfile() 
    {
        CreateMap<OrderEntity, Order>();

        CreateMap<Order, OrderEntity>();
    }
}
