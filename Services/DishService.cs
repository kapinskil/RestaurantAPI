using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantAPI.Entities;
using AutoMapper;
using RestaurantAPI.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Services
{
    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;

        public DishService(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Create(int restaurantId, CreateDishDto dto)
        {
            var restaurant = GetRestaurantById(restaurantId);
            if (restaurant is null)
                throw new NotFoundException("Restaurant not faound");

            var dishEntity = _mapper.Map<Dish>(dto);

            dishEntity.RestaurantId= restaurantId;


            _context.Dishes.Add(dishEntity);
            _context.SaveChanges();

            return dishEntity.Id;
        }

        public DishDto GetById(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            if (restaurant == null)
            {
                throw new NotFoundException("Restaurant not found");
            }

            var dish = _context.Dishes.FirstOrDefault(d => d.Id == dishId);
            if (dish == null || dish.RestaurantId != restaurantId)
            {
                throw new NotFoundException("Dish not found");
            }

            var dishToReturn =_mapper.Map<DishDto>(dish);

            return dishToReturn;
        }

        public IEnumerable<DishDto> GetAll(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            if (restaurant == null)
            {
                throw new NotFoundException("Restaurant not found");
            }

            var dishes = _context.Dishes.Where(d => d.RestaurantId == restaurantId).ToList();

            var dhishesDto = _mapper.Map<IEnumerable<DishDto>>(dishes);

            return dhishesDto;
        }

        public void DeleteAll(int restaurantId)
        {
            var restaurnat = GetRestaurantById(restaurantId);

            if (restaurnat == null)
            {
                throw new NotFoundException("Restaurant not found");
            }

            foreach(var dish in restaurnat.Dishes)
            {
                _context.Remove(dish);
            }

            _context.SaveChanges();  
        }

        public void DeleteById(int restaurantId, int dishId)
        {
            var restaurnat = GetRestaurantById(restaurantId);

            if (restaurnat == null)
            {
                throw new NotFoundException("Restauran not found");
            }

            var dish = _context.Dishes.FirstOrDefault(d => d.Id == dishId);

            if(dish == null || dish.RestaurantId != restaurantId)
            {
                throw new NotFoundException("Dish not found");
            }

            _context.Dishes.Remove(dish);
            _context.SaveChanges();
        }

        private Restaurant GetRestaurantById(int restaurantId)
        {
            var restaurant = _context.Restaurants
                .Include(r => r.Dishes)
                .Include(r => r.Address)
                .FirstOrDefault(r => r.Id == restaurantId);

            return restaurant;
        }

    }
}
