using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceWeatherAPI.Net7.Models;

namespace SpaceWeatherAPI.Net7.Controllers;

[Route("v1/api/[controller]")]
[ApiController]  
public class SatelliteController : Controller
{
    private readonly ApiContext _context;
    public SatelliteController(ApiContext context)
    {
        _context = context;
    }
    // GET: api/Satellites
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<Satellite>>> GetSatellites()
    {
        return await _context.Satellites
            .ToListAsync();
    }
    // POST: api/TodoItems
    [HttpPost("")]
    public async Task<ActionResult<Satellite>> AddSatellite(Satellite satellite)
    {
        _context.Satellites.Add(satellite);
        await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        return CreatedAtAction(nameof(GetSatellites), new { id =satellite.Id }, satellite);
    }
    // GET: api/
    [HttpGet("{id}")]
    public async Task<ActionResult<Satellite>> GetSatelliteById(int id)
    {
        var satellite = await _context.Satellites.FindAsync(id);

        if (satellite == null)
        {
            return NotFound();
        }
        return satellite;
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSatellite(int id, Satellite satellite)
    {
        if (id != satellite.Id)
        {
            return BadRequest();
        }

        _context.Entry(satellite).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SatelliteExists(id))
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
    public async Task<IActionResult> DeleteSatellite(int id)
    {
        var satellite = await _context.Satellites.FindAsync(id);
        if (satellite == null)
        {
            return NotFound();
        }

        _context.Satellites.Remove(satellite);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    private bool SatelliteExists(int id)
    {
        return _context.Satellites.Any(e => e.Id == id);
    }
}
