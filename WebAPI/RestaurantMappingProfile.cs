using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Net;
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
                .ForMember(dest => dest.Addres, opt => opt.MapFrom(src => new Addres
                {
                    City = src.City,
                    PostalCode = src.PostalCode,
                    Street = src.Street
                }))
            .ForMember(dest => dest.Dishes, opt => opt.MapFrom(src =>
                src.Dishes.Select(dish => new Dish
                {
                    Name = dish.Name,
                    Description = dish.Description,
                    Price = dish.Price
                }).ToList()));
            CreateMap<AccountDto, User>()
            .ForMember(u => u.Email, u => u.MapFrom(a => a.Email))
            .ForMember(u => u.Name , u => u.MapFrom(a => a.Name))
            .ForMember(u => u.Password, u => u.MapFrom(a => a.Password));

        }
    }
}
