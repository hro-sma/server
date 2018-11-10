using System.Collections.Generic;

namespace weather.station.server.Models.ViewModels
{
    public class WeatherUpdateViewModel
    {
        public ICollection<WeatherUpdate> LatestUpdates { get; set; }
    }
}
