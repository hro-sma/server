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
                    options.UseSqlServer(connectionString));
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (HostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "api/{controller=WeatherUpdates}/{action=Index}/{id?}");
            });
        }
    }
}
