﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace weather.station.server.Models
{
    public class WeatherUpdate
    { 
        [Key]
        public Guid WeatherUpdateId { get; set; }
        [Required]
        public Guid DeviceId { get; set; }
        public DateTime TimeStamp { get; set; }
        public double TempratureC { get; set; }
        public double Humidity { get; set; }
    }
}