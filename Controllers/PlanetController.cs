using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceWeatherAPI.Net7.Models;

namespace SpaceWeatherAPI.Net7.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class PlanetController : ControllerBase
    {
        private readonly ApiContext _context;
        public PlanetController(ApiContext context)
        {
            _context = context;
        }
        // GET: api/Satellites
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Planet>>> GetPlanets()
        {
            return await _context.Planets
                .ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Planet>> GetPlanetById(int id)
        {
            var planet = await _context.Planets.FindAsync(id);

            if (planet == null)
            {
                return NotFound();
            }
            return planet;
        }
        [HttpPost("")]
        public async Task<ActionResult<Planet>> AddSatellite(Planet planet)
        {
            _context.Planets.Add(planet);
            await _context.SaveChangesAsync();

            //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetPlanets), new { id = planet.Id }, planet);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlanet(int id, Planet planet)
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
        public async Task<IActionResult> DeletePlanet(int id)
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
