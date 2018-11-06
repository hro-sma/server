using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using weather.station.server.Actions;
using weather.station.server.Data;
using weather.station.server.Helpers;
using weather.station.server.Models;
using weather.station.server.Models.ViewModels;

namespace weather.station.server.Controllers.Api
{
    [Route("api/[controller]")]
    public class WeatherUpdatesController : Controller
    {
        private readonly WeatherStationServerContext _context;

        public WeatherUpdatesController(WeatherStationServerContext context)
        {
            _context = context;
        }

        // GET: api/WeatherUpdates
        //Gets all updates from the database
        [HttpGet]
        public async Task<IActionResult> GetWeatherUpdate([FromQuery] DateSelectionViewModel model)
        {
            //Quick null checks. also makes it so the default period is 24hrs
            var fromDate = model.FromDate != null
                ? EpochTimeHelper.EpochToDateTime(model.FromDate.Value)
                : DateTime.Now.Date;
            var toDate = model.ToDate != null
                ? EpochTimeHelper.EpochToDateTime(model.ToDate.Value)
                : DateTime.Now.Date;

            if ((toDate - fromDate).TotalHours > 24)
            {
                return BadRequest();
            }

            //Truncating time in the query.
            var updatesWithingTimeSpan = await _context.WeatherUpdate
                .Where(u => fromDate.Date <= u.TimeStamp.Date && toDate.Date >= u.TimeStamp.Date)
                .OrderByDescending(u => u.TimeStamp)
                .ToListAsync();

            return Ok(updatesWithingTimeSpan);
        }

        // GET: api/WeatherUpdates/5
        // Gets a specific entry from the database
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

        // GET: api/weatherupdates/device/f36962f4-d379-4cce-9537-a897a2608579?FromdDate=1231321&ToDate=12312312312312
        // Gets weatherupdates of a device in a certain timespan.
        [HttpGet("device/{id}")]
        public async Task<IActionResult> GetUpdateFromDevice([FromRoute] Guid id, [FromQuery] DateSelectionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool deviceExists = _context.Device.Any(d => d.DeviceId == id);

            if (!deviceExists)
            {
                return NotFound();
            }

            //Quick null checks. also makes it so the default period is 24hrs
            var fromDate = model.FromDate != null
                ? EpochTimeHelper.EpochToDateTime(model.FromDate.Value)
                : DateTime.Now.Date;
            var toDate = model.ToDate != null
                ? EpochTimeHelper.EpochToDateTime(model.ToDate.Value)
                : DateTime.Now.Date;


            if ((toDate - fromDate).TotalHours > 72)
            {
                return BadRequest();
            }

            //Truncating time in the query.
            var updatesFromDevice = await _context.WeatherUpdate
                .Where(u => u.DeviceId == id && fromDate.Date <= u.TimeStamp.Date && toDate.Date >= u.TimeStamp.Date)
                .OrderByDescending(u => u.TimeStamp)
                .ToListAsync();

            return Ok(updatesFromDevice);
        }

        [HttpGet("device/{id}/latest/{amount}")]
        [RateLimit(10)]
        public async Task<IActionResult> GetLatestUpdate([FromRoute] Guid id, [FromRoute] int amount)
        {
            if (!ModelState.IsValid || amount > 10)
            {
                return BadRequest();
            }

            bool deviceExists = _context.Device.Any(d => d.DeviceId == id);

            if (!deviceExists)
            {
                return NotFound();
            }

            var updates = await _context.WeatherUpdate
                .Where(d => d.DeviceId == id
                           )
                .OrderByDescending(d => d.TimeStamp).Take(amount).ToListAsync();
            return Ok(updates);
        }

        // POST: api/WeatherUpdates
        // Adds an update to the database
        [HttpPost]
        [RateLimit(10)]
        public async Task<IActionResult> PostWeatherUpdate([FromBody] WeatherUpdate weatherUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_context.Device.Any(d => d.DeviceId == weatherUpdate.DeviceId))
            {
                return BadRequest("Device does not exist");
            }

            weatherUpdate.TimeStamp = DateTime.UtcNow;
            weatherUpdate.WeatherUpdateId = Guid.NewGuid();

            _context.WeatherUpdate.Add(weatherUpdate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeatherUpdate", new { id = weatherUpdate.WeatherUpdateId }, weatherUpdate);
        }

        #region futurereference
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

        //private bool WeatherUpdateExists(Guid id)
        //{
        //    return _context.WeatherUpdate.Any(e => e.WeatherUpdateId == id);
        //}
        #endregion
    }
}