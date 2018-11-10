using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace weather.station.server.Models
{
    public class WeatherUpdate
    {
        public Guid WeatherUpdateId { get; set; }
        public Guid DeviceId { get; set; }
        public Device Device { get; set; }
        public DateTime TimeStamp { get; set; }
        public double TemperatureC { get; set; }
        public double Humidity { get; set; }
        public double Windspeed { get; set; }
    }
}