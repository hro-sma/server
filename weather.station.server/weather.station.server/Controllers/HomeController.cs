using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using weather.station.server.Data;

namespace weather.station.server.Controllers
{
    public class HomeController : Controller
    {
        private WeatherStationServerContext _context;

        public HomeController(WeatherStationServerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
