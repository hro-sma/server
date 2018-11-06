using System;
using System.Collections.Generic;
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
            modelBuilder.Entity<Device>()
                .HasKey(d => d.DeviceId);

            modelBuilder.Entity<WeatherUpdate>()
                .HasKey(w => w.WeatherUpdateId);

            modelBuilder.Entity<WeatherUpdate>()
                .HasOne(d => d.Device)
                .WithMany(w => w.WeatherUpdates)
                .HasForeignKey(w => w.DeviceId);

            modelBuilder.Entity<WeatherUpdate>()
                .Property(d => d.DeviceId)
                .IsRequired();

            var testguid = Guid.NewGuid();


            var device = new Device()
            {
                DeviceId = testguid,
                DeviceName = "test",
                Latitude = 5,
                Longitude = 5,
                StudentNumber = "hjdcbs",
            };


            modelBuilder.Entity<Device>().HasData(device);
            var update = new WeatherUpdate()
            {
                DeviceId = testguid,
                Humidity = 5,
                TemperatureC = 5,
                Windspeed = 5,
                WeatherUpdateId = Guid.NewGuid(),
                TimeStamp = DateTime.Now
            };

            modelBuilder.Entity<WeatherUpdate>().HasData(update);
        }

        public DbSet<WeatherUpdate> WeatherUpdate { get; set; }
        public DbSet<Device> Device { get; set; }
    }
}