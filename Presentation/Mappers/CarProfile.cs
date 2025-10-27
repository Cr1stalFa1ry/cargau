using AutoMapper;
using Core.Models;
using db.Entities;

namespace Presentation.Mappers;

public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<CarEntity, Car>().ReverseMap();
    }
}
