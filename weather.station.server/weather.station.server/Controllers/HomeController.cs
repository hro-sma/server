using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult Index()
        {
            var latestPerId = new Dictionary<Guid, WeatherUpdate>();
            if (_context.WeatherUpdate.Any())
            {
                var updates =
                    _context.WeatherUpdate.Include(d => d.Device).GroupBy(i => i.DeviceId); //get updates with the devices
            
                
                foreach (var group in updates)
                {
                    var key = group.Key;
                    var test = group.OrderByDescending(d => d.TimeStamp).First();
                    latestPerId.Add(key, test);
                }
            }

            var latestUpdatesViewModel = new WeatherUpdateViewModel()
            {
                LatestUpdates = latestPerId
            };

            //ViewData["latestPerId"] = latestPerId;

            return View(latestUpdatesViewModel);
        }
    }
}