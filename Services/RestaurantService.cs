using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Exceptions;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
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

        public void Delete(int id)
        {
            _logger.LogWarning($"Restaurant with {id} DELATE action invoked");

            var restaurant = _dbContext
                                .Restaurants
                                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();

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
                throw new NotFoundException("Restaurant not found");
            }

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

            return restaurantDto;
        }

        public void Update(UpdateRestaurantDto dto, int id)
        {
            var restaurant = _dbContext.Restaurants.FirstOrDefault(r => r.Id == id);

            if(restaurant is null)
            {
                throw new NotFoundException("Restauran not found");
            }

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;

            _dbContext.SaveChanges();
        }

    }
}
