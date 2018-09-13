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

namespace weather.station.server
{
    public class Startup
    {
        //SETTING THIS TO FALSE WILL ALLOW ACCESS TO THE PRODUCTION DATABASE
        private const bool UseDevelopmentDatabase = true;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services, IHostingEnvironment env)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            string connectionString;
            if (env.IsDevelopment() && UseDevelopmentDatabase)
            {
                connectionString = Configuration.GetConnectionString("weatherstationserverLocalContext");
            }
            else
            {
                connectionString = Configuration.GetConnectionString("weatherstationserverContext");
            }
            services.AddDbContext<weatherstationserverContext>(options =>
                    options.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "api/{controller=WeatherUpdates}/{action=Index}/{id?}");
            });
        }
    }
}
