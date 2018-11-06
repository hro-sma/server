using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using weather.station.server.Data;
using weather.station.server.Models;

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
            if (_context.WeatherUpdate.Any())
            {
                var updates =
                    _context.WeatherUpdate.Include(d => d.DeviceId).GroupBy(i => i.DeviceId); //get updates with the devices
                var latestPerId = new Dictionary<Guid, WeatherUpdate>();
                foreach (var group in updates)
                {
                    var key = group.Key;
                    var test = group.OrderByDescending(d => d.TimeStamp).First();
                    latestPerId.Add(key, test);
                }
            }
          

            return View();
        }
    }
}