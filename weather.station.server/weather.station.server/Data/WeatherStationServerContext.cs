using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using weather.station.server.Models;

namespace weather.station.server.Data
{
    public class WeatherStationServerContext : DbContext
    {
        public WeatherStationServerContext (DbContextOptions<WeatherStationServerContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<weather.station.server.Models.WeatherUpdate> WeatherUpdate { get; set; }
    }
}