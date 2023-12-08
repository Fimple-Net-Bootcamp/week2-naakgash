using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceWeatherAPI.Net7.Models;

namespace SpaceWeatherAPI.Net7.Controllers;

[Route("v1/api/satellites")]
[ApiController]  
public class SatelliteController : Controller
{
    private readonly ApiContext _context;
    public SatelliteController(ApiContext context)
    {
        _context = context;
    }
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<Satellite>>> GetAll()
    {
        return await _context.Satellites
            .ToListAsync();
    }
    [HttpPost("")]
    public async Task<ActionResult<Satellite>> Create(Satellite satellite)
    {
        _context.Satellites.Add(satellite);
        await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        return CreatedAtAction(nameof(GetById), new { id =satellite.Id }, satellite);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Satellite>> GetById(int id)
    {
        var satellite = await _context.Satellites.FindAsync(id);

        if (satellite == null)
        {
            return NotFound();
        }
        return satellite;
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Satellite satellite)
    {
        if (id != satellite.Id)
        {
            return BadRequest();
        }

        var satelliteDb = await _context.Satellites.FindAsync(id);
        if (satelliteDb == null)
        {
            return NotFound();
        }

        satelliteDb.DistanceFromPlanet = satellite.DistanceFromPlanet;
        satelliteDb.WeatherHistory = satellite.WeatherHistory;
        satelliteDb.Radius = satellite.Radius;
        satelliteDb.Name = satellite.Name;
        satelliteDb.Mass = satellite.Mass;
        satelliteDb.IsTidallyLocked = satellite.IsTidallyLocked;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!SatelliteExists(id))
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
