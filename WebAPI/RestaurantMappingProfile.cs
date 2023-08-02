using AutoMapper;
using WebAPI.Entities;
using WebAPI.Models;

namespace WebAPI
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Addres.City))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Addres.PostalCode))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Addres.Street));

            CreateMap<Dish, DishDto>();

            CreateMap<CreateRestuarantDto, Restaurant>()
                .ForMember(m => m.Addres, c => c.MapFrom(dto => new Addres()
                { City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street }));

        }
    }
}
