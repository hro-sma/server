using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weather.station.server.Models.ViewModels
{
    public class WeatherUpdateViewModel
    {
        public ICollection<WeatherUpdate> LatestUpdates { get; set; }
    }
}
