using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using weather.station.server.Models;

namespace weather.station.server.Controllers
{
    [Route("api/[controller]")]
    public class WeatherUpdatesController : Controller
    {
        private readonly weatherstationserverContext _context;

        public WeatherUpdatesController(weatherstationserverContext context)
        {
            _context = context;
        }

        // GET: api/WeatherUpdates
        [HttpGet]
        public IEnumerable<WeatherUpdate> GetWeatherUpdate()
        {
            return _context.WeatherUpdate;
        }

        // GET: api/WeatherUpdates/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWeatherUpdate([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var weatherUpdate = await _context.WeatherUpdate.FindAsync(id);

            if (weatherUpdate == null)
            {
                return NotFound();
            }

            return Ok(weatherUpdate);
        }

        //[HttpGet]
        //[Route("migrate")]
        //public async Task<IActionResult> GetMigrate()
        //{
        //    await _context.Database.EnsureDeletedAsync();
        //    await _context.Database.MigrateAsync();
        //    return Ok();
        //}

        //// PUT: api/WeatherUpdates/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutWeatherUpdate([FromRoute] Guid id, [FromBody] WeatherUpdate weatherUpdate)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != weatherUpdate.WeatherUpdateId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(weatherUpdate).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!WeatherUpdateExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/WeatherUpdates
        [HttpPost]
        public async Task<IActionResult> PostWeatherUpdate([FromBody] WeatherUpdate weatherUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            weatherUpdate.TimeStamp = DateTime.UtcNow;
            weatherUpdate.WeatherUpdateId = Guid.NewGuid();

            _context.WeatherUpdate.Add(weatherUpdate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeatherUpdate", new { id = weatherUpdate.WeatherUpdateId }, weatherUpdate);
        }

        //// DELETE: api/WeatherUpdates/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteWeatherUpdate([FromRoute] Guid id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var weatherUpdate = await _context.WeatherUpdate.FindAsync(id);
        //    if (weatherUpdate == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.WeatherUpdate.Remove(weatherUpdate);
        //    await _context.SaveChangesAsync();

        //    return Ok(weatherUpdate);
        //}

        private bool WeatherUpdateExists(Guid id)
        {
            return _context.WeatherUpdate.Any(e => e.WeatherUpdateId == id);
        }
    }
}