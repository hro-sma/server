using System;
using System.ComponentModel.DataAnnotations;

namespace weather.station.server.Models.ViewModels
{
    public class DeviceViewModel
    {
        [Required]
        [Display(Name = "Student nummer")]
        [DataType(DataType.Text)]
        public string StudentNumber { get; set; }

        [Required]
        [Display(Name = "Weerstation naam")]
        [DataType(DataType.Text)]
        public string DeviceName { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
	    public double Longitude { get; set; }
	}
}