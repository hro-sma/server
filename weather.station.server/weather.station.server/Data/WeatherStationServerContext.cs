using Microsoft.EntityFrameworkCore;
using weather.station.server.Models;

namespace weather.station.server.Data
{
    public class WeatherStationServerContext : DbContext
    {
        public WeatherStationServerContext(DbContextOptions<WeatherStationServerContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherUpdate>()
                .HasKey(c => c.DeviceId);
            modelBuilder.Entity<Device>()
                .HasMany<WeatherUpdate>();
        }

        public DbSet<WeatherUpdate> WeatherUpdate { get; set; }
        public DbSet<Device> Device { get; set; }
    }
}