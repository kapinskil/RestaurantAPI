using RestaurantAPI.Models;
using System.Collections.Generic;

namespace RestaurantAPI.Services
{
    public interface IDishService
    {
        int Create(int restaurantId, CreateDishDto dto);
        DishDto GetById(int restaurantId, int dishId);
        IEnumerable<DishDto> GetAll(int restayrantId);
        void DeleteAll(int restayrantId);
        void DeleteById(int restayrantId, int dishId);
    }
}