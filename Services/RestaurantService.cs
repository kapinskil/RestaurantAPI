﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _dbContext
                        .Restaurants
                        .Include(r => r.Address)
                        .Include(r => r.Dishes)
                        .ToList();

            var restaurantDtos = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

            return restaurantDtos;
        }

        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();


            return restaurant.Id;
        }

        public bool Delete(int id)
        {
            var restaurant = _dbContext
                                .Restaurants
                                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
            {
                return false;
            }

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();

            return true;
        }


        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext
                               .Restaurants
                               .Include(r => r.Address)
                               .Include(r => r.Dishes)
                               .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
            {
                return null;
            }

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

            return restaurantDto;
        }

        public bool Update(UpdateRestaurantDto dto, int id)
        {
            var restaurant = _dbContext.Restaurants.FirstOrDefault(r => r.Id == id);

            if(restaurant is null)
            {
                return false;
            }

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;

            _dbContext.SaveChanges();

            return true;
        }

    }
}
