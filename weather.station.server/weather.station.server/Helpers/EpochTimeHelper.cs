using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weather.station.server.Helpers
{
    public class EpochTimeHelper
    {
        //Converts epoch time (seconds since epoch) to a proper datetime object
        public static DateTime EpochToDateTime(long epoch)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(epoch);
        }
    }
}
