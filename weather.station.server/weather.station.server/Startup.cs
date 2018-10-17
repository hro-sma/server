using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using weather.station.server.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using weather.station.server.Services;
using Microsoft.AspNetCore.Http;

namespace weather.station.server
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMemoryCache();

            string connectionString;
            if (HostingEnvironment.IsDevelopment())
            {
                connectionString = Configuration.GetConnectionString("WeatherStationServerLocalContext");
                services.AddDbContext<WeatherStationServerContext>(options =>
                    options.UseSqlite(connectionString));
            }
            else
            {
                connectionString = Configuration.GetConnectionString("WeatherStationServerContext");
                services.AddDbContext<WeatherStationServerContext>(options =>
                    options.UseMySql(connectionString));
            }

            // Register services
            services.AddSingleton<IRateLimitService, RateLimitService>();

            // Configure custom services
            services.Configure<RateLimitServiceOptions>(Configuration.GetSection("RateLimit"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (HostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Make sure there is always a X-Forwarded-For header on development environments without a proxy
                app.Use((context, next) =>
                {
                    string forwaredHeader = context.Request.Headers["X-Forwarded-For"];

                    if (forwaredHeader == null)
                    {
                        var clientIp = context.Connection.RemoteIpAddress.ToString();
                        context.Request.Headers["X-Forwarded-For"] = clientIp;
                    }

                    return next.Invoke();
                });
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "api/{controller=WeatherUpdates}/{action=Index}/{id?}");
            });
        }
    }
}
