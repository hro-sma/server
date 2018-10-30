using System;
using System.ComponentModel.DataAnnotations;

namespace weather.station.server.Models
{
    public class Device
    {
        [Key]
        public Guid DeviceId { get; set; }
        public string StudentNumber { get; set; }
        public string DeviceName { get; set; }
        public string Location { get; set; }
    }
}