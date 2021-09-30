using RestaurantAPI.Models;
using System.Collections.Generic;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDto dto);
        RestaurantDto GetById(int id);
        IEnumerable<RestaurantDto> GetAll();
        void Delete(int id);
        void Update(UpdateRestaurantDto dto, int id);

    }
}