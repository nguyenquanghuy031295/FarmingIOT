using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FarmingDatabase.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using ServerFarming.Core.Services;
using ServerFarming.Core.Services.Implement;
using ServerFarming.Core.Repositories;
using ServerFarming.Core.Repositories.Implement;

namespace ServerFarming
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                });
            services.AddCors();
            var connection = @"Server=HNGUYEN;Database=FarmingDatabase;Trusted_Connection=True;";
            services.AddDbContext<FarmingDbContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("ServerFarming")));
            //Add Services
            services.AddTransient<IFarmService, FarmService>();
            services.AddTransient<IPlantService, PlantService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IDeviceService, DeviceService>();

            //Add Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IFarmRepository, FarmRepository>();
            services.AddTransient<IPlantRepository, PlantRepository>();
            services.AddTransient<ISensorRepository, SensorRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:36539")
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            app.UseMvc();
        }
    }
}
