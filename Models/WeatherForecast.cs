using Microsoft.EntityFrameworkCore;

namespace SpaceWeatherAPI.Net7.Models
{
    sealed public class WeatherForecast
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public string? Conditions { get; set; }
    }
}
