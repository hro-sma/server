using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using weather.station.server.Data;
using weather.station.server.Models;
using weather.station.server.Models.ViewModels;

namespace weather.station.server.Controllers
{
    public class RegistrationController : Controller
    {
        private WeatherStationServerContext _context;

        public RegistrationController(WeatherStationServerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // ViewData["Page"] = "HomePages/View";
            return View();
        }

        [HttpGet("/success")]
        public IActionResult Success(string deviceId)
        {
            ViewData["DeviceId"] = deviceId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] DeviceViewModel postData)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            Device device = new Device
            {
                DeviceId = Guid.NewGuid(),
                DeviceName = postData.DeviceName,
                StudentNumber = postData.StudentNumber,
				Latitude = postData.Latitude,
				Longitude = postData.Longitude
            };

            _context.Device.Add(device);
            await _context.SaveChangesAsync();

            return RedirectToAction("Success", new { deviceId = device.DeviceId});
        }
    }
}
