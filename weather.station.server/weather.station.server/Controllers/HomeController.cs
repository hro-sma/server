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
        public IActionResult Index(DateTime dateTime = DateTime.now)
        {

            ICollection<WeatherUpdate> bla = new List<WeatherUpdate>();
            if (_context.WeatherUpdate.Any())
            {
                var updates =
                    _context.WeatherUpdate.Include(d => d.Device).GroupBy(i => i.DeviceId); //get updates with the devices

                
                foreach (var group in updates)
                {

                    var test = group.OrderByDescending(d => d.TimeStamp).where(d => d.TimeStamp < dateTime).First();

                    bla.Add(test);
                }
            }

            var latestUpdatesViewModel = new WeatherUpdateViewModel()
            {
                LatestUpdates = bla

            };

            //ViewData["latestPerId"] = latestPerId;

            return View(latestUpdatesViewModel);
        }
    }
}