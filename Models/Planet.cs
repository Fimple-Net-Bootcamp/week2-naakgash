using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceWeatherAPI.Net7.Models
{
    sealed public class Planet
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Mass { get; set; }
        public double Radius { get; set; }
        public List<WeatherForecast>? WeatherHistory { get; set; }
        public double DistanceFromSun { get; set; }
        public bool HasRings { get; set; }
        public List<Satellite>? Satellites { get; set; }
    }
}
