using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weather.station.server.Models.ViewModels
{
    public class DeviceDashboardViewModel
    {
        public Device Device { get; set; }
        public List<WeatherUpdate> RecentUpdates { get; set; }
        public WeatherUpdate LastUpdate { get; set; }
    }
}
