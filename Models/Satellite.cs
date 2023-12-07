using Microsoft.EntityFrameworkCore;

namespace SpaceWeatherAPI.Net7.Models
{
    sealed public class Satellite
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Mass { get; set; }
        public double Radius { get; set; }
        public List<WeatherForecast>? WeatherHistory { get; set; }
        public double DistanceFromPlanet { get; set; }
        public bool IsTidallyLocked { get; set; }
    }
}
