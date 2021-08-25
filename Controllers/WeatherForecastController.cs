using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantAPI.Services;
using RestaurantAPI.Dtos;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _service;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("generate")]
        public IActionResult Generate([FromQuery]int take, [FromBody]TemperatureParametersDto parameters)
        {
            if (take <= 0 || parameters.Min > parameters.Max) return BadRequest();

            var result = _service.Get(take, parameters.Max, parameters.Min);
            return Ok(result);
        }
    }
}
