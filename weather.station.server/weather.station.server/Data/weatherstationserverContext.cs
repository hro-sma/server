using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using weather.station.server.Models;

    public class weatherstationserverContext : DbContext
    {
        public weatherstationserverContext (DbContextOptions<weatherstationserverContext> options)
            : base(options)
        {
        }

        public DbSet<weather.station.server.Models.WeatherUpdate> WeatherUpdate { get; set; }
    }
