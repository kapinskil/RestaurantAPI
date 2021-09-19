using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        public RestaurantController(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurants = _dbContext
                         .Restaurants
                         .Include(r => r.Address)
                         .Include(r => r.Dishes)
                         .ToList();

            var restaurantDto = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

            return Ok(restaurantDto);
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _dbContext
                                .Restaurants
                                .Include(r => r.Address)
                                .Include(r => r.Dishes)
                                .FirstOrDefault(r => r.Id == id);

            if(restaurant is null)
            {
                return NotFound();
            }

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

            return Ok(restaurantDto);
        }
    }
}
