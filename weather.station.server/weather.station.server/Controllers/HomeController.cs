using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using weather.station.server.Data;
using weather.station.server.Models;
using weather.station.server.Models.ViewModels;

namespace weather.station.server.Controllers
{
    public class HomeController : Controller
    {
        private readonly WeatherStationServerContext _context;

        public HomeController(WeatherStationServerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(DateTime? sliderTime)
        {
            var time = sliderTime ?? DateTime.Now; //if date time is null, datetime is now

            ICollection<WeatherUpdate> weatherUpdates = new List<WeatherUpdate>();

            if (_context.WeatherUpdate.Any())
            {
                var updates = _context.WeatherUpdate
                    .Include(d => d.Device)
                    .GroupBy(i => i.DeviceId);

                weatherUpdates = updates
                    .Select(g => g.OrderByDescending(d => d.TimeStamp).First(d => d.TimeStamp < time))
                    .ToList();
            }

            var latestUpdatesViewModel = new WeatherUpdateViewModel
            {
                LatestUpdates = weatherUpdates
            };

            return View(latestUpdatesViewModel);
        }
    }
}