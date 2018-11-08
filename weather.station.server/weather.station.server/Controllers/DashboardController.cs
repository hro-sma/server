using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using weather.station.server.Data;
using weather.station.server.Helpers;
using weather.station.server.Models.ViewModels;

namespace weather.station.server.Controllers
{
    [Route("[controller]")]
    public class DashboardController : Controller
    {
        private readonly WeatherStationServerContext _context;

        public DashboardController(WeatherStationServerContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ActionName("Index")]
        [HttpGet("{deviceId}")]
        public IActionResult Dashboard(Guid deviceId, [FromRoute] DateSelectionViewModel dateSelectionViewModel)
        {
            var device = _context.Device.AsNoTracking().SingleOrDefault(r => r.DeviceId == deviceId);
            
            if (device == null)
            {
                return View("Index", new DeviceSearchViewModel
                {
                    ErrorMessage = string.Format("Device: {0} niet gevonden.", deviceId),
                    DeviceId =  deviceId
                });
            }

            //Quick null checks. also makes it so the default period is 24hrs
            var fromDate = dateSelectionViewModel.FromDate != null
                ? EpochTimeHelper.EpochToDateTime(dateSelectionViewModel.FromDate.Value)
                : DateTime.Now;
            var toDate = dateSelectionViewModel.ToDate != null
                ? EpochTimeHelper.EpochToDateTime(dateSelectionViewModel.ToDate.Value)
                : DateTime.Now;


            var updatesForDevice = _context.WeatherUpdate.AsNoTracking()
                .Where(r => r.DeviceId == deviceId && r.TimeStamp < toDate)
                .OrderByDescending(r => r.TimeStamp)
                .Take(20) //Only take 20 max.
                .OrderBy(r => r.TimeStamp).ToList(); // order back for chartjs

            var mostRecentUpdate = _context.WeatherUpdate.AsNoTracking().Where(r => r.DeviceId == deviceId)
                .OrderByDescending(r => r.TimeStamp).FirstOrDefault();

            //Construct viewmodel
            var dashboardViewModel = new DeviceDashboardViewModel
            {
                Device = device,
                RecentUpdates = updatesForDevice,
                LastUpdate = mostRecentUpdate
            };

            return View("Dashboard", dashboardViewModel);
        }

        [HttpPost]
        public IActionResult Search(DeviceSearchViewModel viewModel)
        {   
            //Er kan hier een mooiere redirect terug komen waar de device niet gevonden is ofzo ipv dat laten afhandelen door de index + params method
            return RedirectToAction("Index", new {deviceId = viewModel.DeviceId});
        }    
    }
}
