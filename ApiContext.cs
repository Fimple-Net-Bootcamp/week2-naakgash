using Microsoft.EntityFrameworkCore;
using SpaceWeatherAPI.Net7.Models;

namespace SpaceWeatherAPI.Net7;

public class ApiContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("CelestialBodyDB");
    }

    public DbSet<Planet> Planets { get; set; } = null!;
    public DbSet<Satellite> Satellites { get; set; } = null!;
    public DbSet<WeatherForecast> WeatherData { get; set; } = null!;
}
