using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceWeatherAPI.Net7.Models;

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

            _context.Entry(planet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
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
