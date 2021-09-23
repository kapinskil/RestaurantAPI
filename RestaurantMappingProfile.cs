using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI
{
    public class RestaurantMappingProfile: Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(r => r.City, c => c.MapFrom(a => a.Address.City))
                .ForMember(r => r.Street, c => c.MapFrom(a => a.Address.Street))
                .ForMember(r => r.PostCode, c => c.MapFrom(a => a.Address.PostCode))
                .ForMember(r => r.DishDtos, c => c.MapFrom(a => a.Dishes));

            CreateMap<Dish, DishDto>();

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(r => r.Address,
                c => c.MapFrom(restaurant =>
                                new Address() { City = restaurant.City, PostCode = restaurant.PostCode, Street = restaurant.Street }));

            CreateMap<UpdateRestaurantDto, Restaurant>();
        }   
    }
}
