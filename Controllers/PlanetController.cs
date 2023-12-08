using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceWeatherAPI.Net7.Models;
using System.Numerics;

namespace SpaceWeatherAPI.Net7.Controllers
{
    [Route("v1/api/planets")]
    [ApiController]
    public class PlanetController : ControllerBase
    {
        private readonly ApiContext _context;
        public PlanetController(ApiContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Planet>>> GetAll()
        {
            return await _context.Planets
                .ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Planet>> GetById(int id)
        {
            var planet = await _context.Planets.FindAsync(id);

            if (planet == null)
            {
                return NotFound();
            }
            return planet;
        }
        [HttpPost("")]
        public async Task<ActionResult<Planet>> Create(Planet planet)
        {
            _context.Planets.Add(planet);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = planet.Id }, planet);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Planet planet)
        {
            if (id != planet.Id)
            {
                return BadRequest();
            }

            var planetDb = await _context.Planets.FindAsync(id);
            if (planetDb == null)
            {
                return NotFound();
            }

            planetDb.DistanceFromSun = planet.DistanceFromSun;
            planetDb.WeatherHistory = planet.WeatherHistory;
            planetDb.Satellites = planet.Satellites;
            planetDb.Radius = planet.Radius;
            planetDb.HasRings = planet.HasRings;
            planetDb.Name = planet.Name;
            planetDb.Mass = planet.Mass;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!PlanetExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, JsonPatchDocument<Planet> patchDoc)
        {
            var planetDb = await _context.Planets.FindAsync(id);
            if (planetDb == null)
            {
                return NotFound();
            }
            patchDoc.ApplyTo(planetDb);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var planet = await _context.Planets.FindAsync(id);
            if (planet == null)
            {
                return NotFound();
            }

            _context.Planets.Remove(planet);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool PlanetExists(int id)
        {
            return _context.Planets.Any(e => e.Id == id);
        }
    }
}
