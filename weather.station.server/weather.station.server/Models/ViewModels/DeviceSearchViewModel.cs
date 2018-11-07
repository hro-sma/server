using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace weather.station.server.Models.ViewModels
{
    public class DeviceSearchViewModel
    {
        public Guid DeviceId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
