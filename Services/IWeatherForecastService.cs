﻿using System.Collections.Generic;

namespace RestaurantAPI.Services
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Get(int take, int max, int min);
    }
}