using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weather.station.server.Models.ViewModels
{
    public class DateSelectionViewModel
    {

        public long? FromDate { get; set; }
        public long? ToDate { get; set; }
    }
}
